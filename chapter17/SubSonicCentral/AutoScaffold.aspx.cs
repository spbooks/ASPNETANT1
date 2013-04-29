using System;
using System.Data;
using System.Collections;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SubSonic;
using SubSonic.Controls;
using SubSonic.Utilities;

public partial class AutoScaffold : Page
{

    private readonly string _manyToManyMap = String.Empty;
    private readonly Button btnSave = new Button();
    private readonly Button btnCancel = new Button();
    private readonly Button btnDelete = new Button();
    private readonly Button btnAdd = new Button();
    private const string SORT_DIRECTION = "SORT_DIRECTION";
    private const string ORDER_BY = "ORDER_BY";
    private const string PROVIDER_NAME = "AS_PROVIDER";
    private const string TABLE_NAME = "AS_TABLE";

    protected void Page_Load(object sender, EventArgs e)
    {

        if(Request["refresh"] != null)
        {
            DataService.ResetDatabases();
            Response.Redirect(Request.Path);
        }

        if (!Page.IsPostBack)
        {
            if (DataService.Providers != null)
            {
                ddlProviders.Items.Clear();
                foreach(DataProvider p in DataService.Providers)
                {
                    ddlProviders.Items.Add(p.Name);
                }
            }

        }

        if(ddlProviders.Items.Count > 0)
        {
            ddlProviders.Visible = true;
            pnlError.Visible = false;
            tblWrapper.Visible = true;
            
        }
        else
        {
            ddlProviders.Visible = false;
            pnlError.Visible = true;
            tblWrapper.Visible = false;
        }
        SetupPage();

    }

    private void SetupPage()
    {
        BuildTableList();
        pnlButtons.Controls.Clear();
        if (HttpContext.Current.Request.QueryString["id"] != null)
        {
            //add in the control bar
            //add in the button row
            pnlGridView.Visible = false;
            pnlDetail.Visible = true;
            //pnlDetail.Controls.Clear();

            btnAdd.ID = "btnAdd";
            pnlButtons.Controls.Add(btnAdd);

            btnSave.ID = "btnSave";
            pnlButtons.Controls.Add(btnSave);

            btnCancel.ID = "btnCancel";
            pnlButtons.Controls.Add(btnCancel);

            btnDelete.ID = "btnDelete";
            pnlButtons.Controls.Add(btnDelete);

            foreach (Button button in pnlButtons.Controls)
            {
                ApplyCssClass(button, "scaffoldButton");
            }

            btnDelete.OnClientClick = "return CheckDelete();";

            btnSave.Text = "Save";
            //btnSaveAndReturn.Text = "Save & Return";
            btnDelete.Text = "Delete";
            btnCancel.Text = "Return";
            btnAdd.Text = "Add";

            btnAdd.Click += btnAdd_Click;
            btnSave.Click += btnSave_Click;
            //btnSaveAndReturn.Click += new EventHandler(btnSaveAndReturn_Click);
            btnCancel.Click += btnCancel_Click;
            btnDelete.Click += btnDelete_Click;

            string keyID = HttpContext.Current.Request.QueryString["id"];
            tblEditor.Rows.Clear();
            if (keyID != string.Empty && keyID != "0")
            {
                CreateEditor(true);
                BindEditor(keyID);
            }
            else
            {
                CreateEditor(false);
                btnAdd.Visible = false;
                btnDelete.Visible = false;
            }

            Label lblMessage = new Label();
            lblMessage.ID = "lblMessage";
            pnlDetail.Controls.Add(lblMessage);

        }
        else
        {
            pnlGridView.Visible = true;
            pnlDetail.Visible = false;
            BindGrid(String.Empty);

            btnAdd.ID = "btnAdd";
            pnlButtons.Controls.Add(btnAdd);
            btnAdd.Text = "Add";
            ApplyCssClass(btnAdd, "scaffoldButton");
            btnAdd.Click += btnAdd_Click;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        WebUIHelper.EmitClientScripts(Page);
        if (!String.IsNullOrEmpty(ProviderName))
        {
            ddlProviders.SelectedValue = ProviderName;
        }
        base.OnPreRender(e);
    }

    private TableSchema.Table _schema;
    private TableSchema.Table Schema
    {
        get
        {
            if (_schema == null || !Utility.IsMatch(_schema.Name, TableName) || !Utility.IsMatch(_schema.Provider.Name, ProviderName))
            {
                _schema = DataService.GetTableSchema(TableName, ProviderName, TableType.Table);
            }
            return _schema;
        }
    }

    private string _providerName;
    private string ProviderName
    {
        get
        {
            if (Session[PROVIDER_NAME] != null)
            {
                _providerName = Session[PROVIDER_NAME].ToString();
            }
            else
            {
                Session.Add(PROVIDER_NAME, ddlProviders.SelectedValue);
                _providerName = ddlProviders.SelectedValue;
            }
            return _providerName;
        }

        set
        {
            _providerName = value;
            Session.Add(PROVIDER_NAME, _providerName);
            //Session.Remove(TABLE_NAME);
        }
    }

    private string _tableName;
    private string TableName
    {
        get
        {
            if (Session[ProviderName + TABLE_NAME] != null)
            {
                _tableName = Session[ProviderName + TABLE_NAME].ToString();
            }
            else
            {
                _tableName = null;
            }
            return _tableName;
        }
    }

    private void SetTableName(TableSchema.AbstractTableSchema tblSchema)
    {
        _tableName = tblSchema.Name;
        Session.Add(ProviderName + TABLE_NAME, _tableName);
        lblHeader.Text = tblSchema.DisplayName;
    }

    private void SetTableName(string tableName)
    {
        TableSchema.Table schema = DataService.GetTableSchema(tableName, ProviderName, TableType.Table);
        SetTableName(schema);
    }

    private void BuildTableList()
    {
        pnlTableList.Controls.Clear();
        TableSchema.Table[] tables = DataService.GetTables(ProviderName);
        foreach (TableSchema.Table tblSchema in tables)
        {
            if (CodeService.ShouldGenerate(tblSchema.Name, tblSchema.Provider.Name))
            {
                LiteralControl lcDivStart = new LiteralControl("<div style=\"padding:2px 2px 4px 2px\">");
                pnlTableList.Controls.Add(lcDivStart);
                LinkButton lb = new LinkButton();
                lb.ID = "lbt" + tblSchema.Name;
                pnlTableList.Controls.Add(lb);
                lb.Text = tblSchema.DisplayName;

                if(tblSchema.PrimaryKey != null && tblSchema.PrimaryKeys.Length > 0)
                {
                    lb.CommandName = tblSchema.Name;
                    lb.CommandArgument = tblSchema.DisplayName;
                    lb.Click += lb_Click;
                }
                else
                {
                    lb.Enabled = false;
                }
                LiteralControl lcDivEnd = new LiteralControl("</div>");
                pnlTableList.Controls.Add(lcDivEnd);
            }
        }
        if (tables.Length > 0 && String.IsNullOrEmpty(TableName))
        {
            for (int i = 0; i < tables.Length; i++)
            {
                if (tables[i].PrimaryKey != null && CodeService.ShouldGenerate(tables[i].Name, tables[i].Provider.Name))
                {
                    SetTableName(tables[i]);
                    break;
                }
            }
        }
    }

    private void BindGrid(string orderBy)
    {
        //TableSchema.Table schema = DataService.GetTableSchema(tableName, providerName);
        if (Schema != null && Schema.PrimaryKey != null)
        {
            SetTableName(Schema);
            Query query = new Query(Schema);

            string sortColumn = null;
            if(!String.IsNullOrEmpty(orderBy))
            {
                sortColumn = orderBy;
            }
            else if(ViewState[ORDER_BY] != null)
            {
                sortColumn = (string)ViewState[ORDER_BY];
            }

            int colIndex = -1;

            if (!String.IsNullOrEmpty(sortColumn))
            {
                ViewState.Add(ORDER_BY, sortColumn);
                TableSchema.TableColumn col = Schema.GetColumn(sortColumn);
                if(col == null)
                {
                    for (int i = 0; i < Schema.Columns.Count; i++)
                    {
                        TableSchema.TableColumn fkCol = Schema.Columns[i];
                        if(fkCol.IsForeignKey && !String.IsNullOrEmpty(fkCol.ForeignKeyTableName))
                        {
                            TableSchema.Table fkTbl = DataService.GetSchema(fkCol.ForeignKeyTableName, ProviderName, TableType.Table);
                            if(fkTbl != null)
                            {
                                col = fkTbl.Columns[1];
                                colIndex = i;
                                break;
                            }
                        }
                    }
                }
                if(col != null && col.MaxLength < 2048)
                {
                    if (ViewState[SORT_DIRECTION] == null || ((string)ViewState[SORT_DIRECTION]) == SqlFragment.ASC)
                    {
                        if (colIndex > -1)
                        {
                            query.OrderBy = OrderBy.Asc(col, SqlFragment.JOIN_PREFIX + colIndex);
                        }
                        else
                        {
                            query.OrderBy = OrderBy.Asc(col);
                        }
                        ViewState[SORT_DIRECTION] = SqlFragment.ASC;
                    }
                    else
                    {
                        if (colIndex > -1)
                        {
                            query.OrderBy = OrderBy.Desc(col, SqlFragment.JOIN_PREFIX + colIndex);
                        }
                        else
                        {
                            query.OrderBy = OrderBy.Desc(col);
                        }
                        ViewState[SORT_DIRECTION] = SqlFragment.DESC;
                    }
                }
            }
           
            
            DataTable dt = query.ExecuteJoinedDataSet().Tables[0];
            grid.DataSource = dt;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            HyperLinkField link = new HyperLinkField();
            link.Text = "Edit";
            link.DataNavigateUrlFields = new string[] { Schema.PrimaryKey.ColumnName };
            link.DataNavigateUrlFormatString = HttpContext.Current.Request.CurrentExecutionFilePath + "?id={0}";
            grid.Columns.Insert(0, link);

            for (int i = 0; i < Schema.Columns.Count; i++ )
            {

                BoundField field = new BoundField();
                field.DataField = dt.Columns[i].ColumnName;
                field.SortExpression = dt.Columns[i].ColumnName;
                //field.SortExpression = Utility.QualifyColumnName(Schema.Name, dt.Columns[i].ColumnName, Schema.ProviderType);
                field.HtmlEncode = false;
                if (Schema.Columns[i].IsForeignKey)
                {
                    TableSchema.Table schema;
                    if(Schema.Columns[i].ForeignKeyTableName == null)
                    {
                        schema = DataService.GetForeignKeyTable(Schema.Columns[i], Schema);
                    }
                    else
                    {
                        schema = DataService.GetSchema(Schema.Columns[i].ForeignKeyTableName, ProviderName, TableType.Table);
                    }
                    if (schema != null)
                    {
                        field.HeaderText = schema.DisplayName;
                    }
                }
                else
                {
                    field.HeaderText = Schema.Columns[i].DisplayName;
                }

                field.HeaderText = CleanColumnName(field.HeaderText);

                if (!Utility.IsAuditField(dt.Columns[i].ColumnName))
                {
                    grid.Columns.Add(field);
                }
            }

            grid.DataBind();
        }
    }

    /// <summary>
    /// Used to apply CSS class values to WebControls. Ensures that no empty classes are applied;
    /// </summary>
    /// <param name="control"></param>
    /// <param name="cssClass"></param>
    private static void ApplyCssClass(WebControl control, string cssClass)
    {
        if (!String.IsNullOrEmpty(cssClass))
        {
            control.CssClass = cssClass;
        }
    }


    /// <summary>
    /// Used to apply class attribute to HtmlControls. Ensures that no empty classes are applied;
    /// </summary>
    /// <param name="control"></param>
    /// <param name="cssClass"></param>
    private static void ApplyCssClass(HtmlControl control, string cssClass)
    {
        if (!String.IsNullOrEmpty(cssClass))
        {
            control.Attributes.Add("class", cssClass);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tbl"></param>
    /// <param name="text"></param>
    /// <param name="colspan"></param>
    private static void AddRow(HtmlTable tbl, string text, int colspan)
    {
        HtmlTableRow tr = new HtmlTableRow();
        tbl.Rows.Add(tr);

        HtmlTableCell td = new HtmlTableCell();
        tr.Cells.Add(td);
        ApplyCssClass(td, "scaffoldEditItem");

        if (colspan > 0)
        {
            td.ColSpan = colspan;
        }
        td.InnerHtml = text;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tbl"></param>
    /// <param name="cellValue1"></param>
    /// <param name="control"></param>
    private static void AddRow(HtmlTable tbl, string cellValue1, Control control)
    {
        HtmlTableRow tr = new HtmlTableRow();
        tbl.Rows.Add(tr);

        HtmlTableCell td = new HtmlTableCell();
        tr.Cells.Add(td);

        //label
        ApplyCssClass(td, "scaffoldEditItemCaption");
        td.InnerHtml = "<b>" + cellValue1 + "</b>";

        //control
        HtmlTableCell td2 = new HtmlTableCell();
        tr.Cells.Add(td2);
        ApplyCssClass(td, "scaffoldEditItemCaption");
        td2.Controls.Add(control);
    }

    /// <summary>
    /// Special builder for many to many relational tables.
    /// </summary>
    /// <returns></returns>
    private void CreateManyMapper(TableSchema.Table schema)
    {
        //add a header row
        AddRow(tblEditor, "<h2>" + schema.DisplayName + " Map </h2>", 2);

        foreach (TableSchema.TableColumn col in schema.Columns)
        {
            //by convention, each key in the map table should be a foreignkey
            //if not, it's not good
            if (col.IsPrimaryKey && col.IsForeignKey)
            {
                string fkTable = col.ForeignKeyTableName;
                //fkTable = DataService.GetForeignKeyTableName(col.ColumnName, schema.Name, ProviderName);
                if (!String.IsNullOrEmpty(fkTable))
                {
                    //Query qry = new Query(DataService.GetTableSchema(fkTable, ProviderName, TableType.Table));
                    TableSchema.Table fkSchema = DataService.GetTableSchema(fkTable, ProviderName, TableType.Table);
                    Query qry = new Query(fkSchema).ORDER_BY(fkSchema.Columns[1]);
                    DropDownList ddl = new DropDownList();
                    ddl.ID = col.ColumnName;
                    AddRow(tblEditor, fkTable, ddl);

                    IDataReader rdr = qry.ExecuteReader();
                    while (rdr.Read())
                    {
                        ddl.Items.Add(new ListItem(rdr[1].ToString(), rdr[0].ToString()));
                    }
                    rdr.Close();
                }
            }
            else
            {
                Control ctrl = GetEditControl(col);
                AddRow(tblEditor, Utility.ParseCamelToProper(col.ColumnName), ctrl);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static bool isManyToMany(TableSchema.Table schema)
    {
        int keyCount = 0;
        bool bOut = false;

        foreach (TableSchema.TableColumn col in schema.Columns)
        {
            if (col.IsPrimaryKey)
            {
                keyCount++;
            }
        }

        if (keyCount > 1)
        {
            bOut = true;
        }
        return bOut;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isEdit"></param>
    /// <returns></returns>
    private void CreateEditor(bool isEdit)
    {
        //if this is a many to many, we need to construct it differently
        //tbl = new HtmlTable();
        //tbl.ID = "tblEditor";
        //pnlDetail.Controls.Add(tbl);
        if (isManyToMany(Schema))
        {
            CreateManyMapper(Schema);
        }
        else
        {

            //add a header row
            //AddRow(tbl, "<h2>" + schema.DisplayName + " Editor</h2>", 2);

            foreach (TableSchema.TableColumn col in Schema.Columns)
            {
                Control ctrl = GetEditControl(col);
                if (ctrl != null)
                {
                    string label = col.DisplayName;
                    //label = Utility.ParseCamelToProper(col.ColumnName);
                    //label = CleanColumnName(label);
                    AddRow(tblEditor, label, ctrl);
                    if (ctrl.GetType() == typeof(TextBox))
                    {
                        TextBox tbx = (TextBox)ctrl;
                        if (tbx.TextMode == TextBoxMode.MultiLine)
                        {
                            int efftectiveMaxLength = Utility.GetEffectiveMaxLength(col);
                            string remainingLength = (efftectiveMaxLength - tbx.Text.Length).ToString();
                            string maxLength = efftectiveMaxLength.ToString();

                            tbx.Attributes.Add("onkeyup", "return imposeMaxLength(event, this, " + maxLength + ", " + tblEditor.Rows.Count + ");");
                            tbx.Attributes.Add("onChange", "return imposeMaxLength(event, this, " + maxLength + ", " + tblEditor.Rows.Count + ");");
                            LiteralControl lc = new LiteralControl("<div style='padding: 2px;'><div style='float:left'>Characters Remaining:&nbsp;</div><div id=\"counter" + tblEditor.Rows.Count + "\" style=\"visibility:hidden\">" + remainingLength + "</div></div>");
                            tbx.Parent.Controls.Add(lc);
                        }
                    }
                    //else if(ctrl is CalendarControl)
                    //{
                    //    ((Calendar)ctrl).SelectionChanged += new EventHandler(cal_SelectionChanged);
                    //}
                }
            }
            //need a primary key for many/many editing
            if (!String.IsNullOrEmpty(_manyToManyMap) && isEdit)
            {
                AddManyToMany(isEdit, tblEditor);
            }
        }
    }

    /// <summary>
    /// Converts column name to a value appropriate for display. 
    /// Removes "ID" from the end of columns, can make other changes.
    /// </summary>
    /// <param name="columnName"></param>
    /// <returns></returns>
    private static string CleanColumnName(string columnName)
    {
        //if (!CLEAN_COLUMN_NAMES)
            return columnName;

        //if(columnName.EndsWith("ID") && columnName.Length > 2)
        //{
        //    columnName = columnName.Remove(columnName.Length - 2);
        //}
        //return columnName;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyID"></param>
    private void BindEditor(string keyID)
    {
        //get all the data for this row
        Query qry = new Query(Schema);
        qry.AddWhere(Schema.PrimaryKey.ColumnName, keyID);
        IDataReader rdr = qry.ExecuteReader();

        if (rdr.Read())
        {
            foreach (TableSchema.TableColumn col in Schema.Columns)
            {
                if (col.IsPrimaryKey && !col.IsForeignKey)
                {
                    Control ctrl = tblEditor.FindControl("pkID");
                    if (ctrl != null)
                    {
                        Type ctrlType = ctrl.GetType();

                        string colValue = rdr[col.ColumnName].ToString();
                        if(ctrlType == typeof(Label))
                        {
                            ((Label)ctrl).Text = colValue;
                        }
                        else if(ctrlType == typeof(DropDownList))
                        {
                            ((DropDownList)ctrl).SelectedValue = colValue;
                        }
                        else if(ctrlType == typeof(TextBox))
                        {
                            ((TextBox)ctrl).Text = colValue;
                        }
                    }
                }
                else
                {
                    Control ctrl = tblEditor.FindControl(col.ColumnName);
                    if (ctrl != null)
                    {
                        Type ctrlType = ctrl.GetType();
                        if(ctrlType == typeof(TextBox))
                        {
                            TextBox tbx = ((TextBox)ctrl);
                            tbx.Text = rdr[col.ColumnName].ToString();
                        }
                        else if(ctrlType == typeof(CheckBox))
                        {
                            if(!col.IsNullable || (col.IsNullable && rdr[col.ColumnName] != DBNull.Value))
                            {
                                ((CheckBox)ctrl).Checked = Convert.ToBoolean(rdr[col.ColumnName]);
                            }
                        }
                        else if(ctrlType == typeof(DropDownList))
                        {
                            ((DropDownList)ctrl).SelectedValue = rdr[col.ColumnName].ToString();
                        }
                        else if(ctrlType == typeof(CalendarControl))
                        {

                            CalendarControl cal = (CalendarControl)ctrl;
                            DateTime dt;
                            if(ViewState["vs" + cal.ID] != null)
                            {
                                dt = (DateTime)ViewState["vs" + cal.ID];
                            }
                            else
                            {
                                DateTime.TryParse(rdr[col.ColumnName].ToString(), out dt);
                            }
                            cal.SelectedDate = dt.Date;
                            //cal.VisibleDate = dt.Date;
                            ViewState["vs" + cal.ID] = dt;
                        }
                        else if(ctrlType == typeof(Label))
                        {
                            ((Label)ctrl).Text = rdr[col.ColumnName].ToString();
                        }
                    }
                }
            }
        }
    }

    //void cal_SelectionChanged(object sender, EventArgs e)
    //{
    //    CalendarControl cal = (CalendarControl)sender;
    //    ViewState.Add("vs" + cal.ID, cal.SelectedDate.Date);
    //    //throw new Exception("The method or operation is not implemented.");
    //}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isEdit"></param>
    /// <param name="table"></param>
    private void AddManyToMany(bool isEdit, HtmlTable table)
    {
        string[] mmTables = _manyToManyMap.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        if (mmTables.Length > 0)
        {
            foreach (string mmTableName in mmTables)
            {
                TableSchema.Table mmTable = Query.BuildTableSchema(mmTableName);

                //this table should have one or more primary keys
                //one of these keys should, by convention, have the same name
                //as the primary key of our main schema table
                //need to get this key, then find it's table
                foreach (TableSchema.TableColumn col in mmTable.Columns)
                {
                    if (col.IsPrimaryKey && col.ColumnName.ToLower() != Schema.PrimaryKey.ColumnName.ToLower())
                    {
                        //this is the key we need. Get the table for this key
                        string fTableName = DataService.GetForeignKeyTableName(col.ColumnName, mmTableName, ProviderName);

                        if (fTableName != string.Empty && isEdit)
                        {
                            CheckBoxList chk = new CheckBoxList();
                            chk.ID = mmTableName;
                            //add the checkbox in
                            AddRow(table, Utility.ParseCamelToProper(mmTableName), chk);
                            chk.RepeatColumns = 2;

                            TableSchema.Table fTable = Query.BuildTableSchema(fTableName);
                            Query qry = new Query(fTable);
                            IDataReader rdr = qry.ExecuteReader();

                            while (rdr.Read())
                            {
                                chk.Items.Add(new ListItem(rdr[1].ToString(), rdr[0].ToString()));
                            }

                            rdr.Close();
                            //now we need to query the map table, loop it, and check off the items
                            //that are in it

                            object pk = Context.Request.QueryString["id"];

                            rdr = new Query(mmTable).AddWhere(Schema.PrimaryKey.ColumnName, pk).ExecuteReader();

                            //thanks to jcoenen for this!
                            while (rdr.Read())
                            {
                                string fkID = rdr[fTable.PrimaryKey.ColumnName].ToString();
                                foreach (ListItem item in chk.Items)
                                {
                                    if (item.Value.ToLower().Equals(fkID.ToLower()))
                                    {
                                        item.Selected = true;
                                        break;
                                    }

                                }

                            }
                            rdr.Close();

                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mapTableName"></param>
    private void SaveManyToMany(string mapTableName)
    {
        //first, need to get the id of the other field
        TableSchema.Table fkTable = Query.BuildTableSchema(mapTableName);
        string fkField = String.Empty;
        foreach (TableSchema.TableColumn col in fkTable.Columns)
        {
            if (col.IsPrimaryKey && col.ColumnName.ToLower() != Schema.PrimaryKey.ColumnName.ToLower())
            {
                fkField = col.ColumnName;
                break;
            }
        }

        if (fkField != string.Empty)
        {
            int pk = Convert.ToInt32(Context.Request.QueryString["id"]);
            //first, delete out all references in there
            //this MUST be done in a transaction!
            QueryCommandCollection transCollection = new QueryCommandCollection();


            Query qry = new Query(DataService.GetTableSchema(mapTableName, ProviderName, TableType.Table));
            qry.QueryType = QueryType.Delete;
            qry.AddWhere(Schema.PrimaryKey.ColumnName, pk);

            transCollection.Add(qry.BuildDeleteCommand());

            //now, loop the check list, adding items in for each checked bit
            string sql = "INSERT INTO " + mapTableName + "(" + fkField + "," + Schema.PrimaryKey.ColumnName + ") VALUES (" + Utility.PrefixParameter("fk", Schema.Provider) + "," + Utility.PrefixParameter("pk)", Schema.Provider);

            CheckBoxList chk = (CheckBoxList)tblEditor.FindControl(fkTable.Name);
            if (chk != null)
            {
                foreach (ListItem item in chk.Items)
                {
                    if (item.Selected)
                    {
                        QueryCommand cmd = new QueryCommand(sql, ProviderName);
                        cmd.Parameters.Add(Utility.PrefixParameter("fk", Schema.Provider), item.Value, DbType.Int32);
                        cmd.Parameters.Add(Utility.PrefixParameter("pk", Schema.Provider), pk);
                        transCollection.Add(cmd);
                    }
                }
            }

            //execute
            DataService.ExecuteTransaction(transCollection);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void SaveEditor()
    {

        QueryCommand cmd = new QueryCommand(String.Empty, ProviderName);
        string editID = String.Empty;

        bool isAdd = false;

        if (Context.Request.QueryString["id"] != null)
        {
            editID = Context.Request.QueryString["id"];
        }

        if ((editID == string.Empty || editID == "0"))
        {
            isAdd = true;
        }
        //gotta loop through here, create the proper command for this table
        //and execute
        //see if lblID is 0 or a value
        //if the primary key of the schema table is autoincrement, this will be a label

        //FUTURE: Load or Instance under
        //Assembly appCode = Assembly.Load("App_Code");
        //string strType = Schema.Provider.GeneratedNamespace + "." + Schema.ClassName;
        //Type scaffoldType =  appCode.GetType(strType);
        //scaffoldType.UnderlyingSystemType.GetProperty(Schema.Columns[0].PropertyName);
        //AppDomain.CurrentDomain.CreateInstanceFromAndUnwrap(Assembly.Load("App_Code").Location, Schema.Provider.GeneratedNamespace + "." + Schema.ClassName)

        object pk = null;


        Control c = tblEditor.FindControl("pkID");
        Type ctrlType = c.GetType();
        if (ctrlType != null)
        {
            if(ctrlType == typeof(Label))
            {
                pk = ((Label)c).Text;
            }
            else if(ctrlType == typeof(TextBox))
            {
                pk = ((TextBox)c).Text;
            }
        }

        if (pk != null)
        {
            if(!isAdd)
            {
                cmd.CommandSql = BuildUpdateSql(Schema);
            }
            else
            {
                cmd.CommandSql = BuildInsertSql(Schema);
            }

            //string pkParameter = Utility.PrefixParameter(Schema.PrimaryKey.ParameterName, Schema.ProviderType);
            foreach(TableSchema.TableColumn col in Schema.Columns)
            {
                //pull the value from the controls
                string colParameter = Utility.PrefixParameter(col.ParameterName, Schema.Provider);
                if(col.DataType != DbType.Binary && col.DataType != DbType.Byte)
                {
                    if(col.IsPrimaryKey && !col.IsForeignKey)
                    {
                        if(!isAdd)
                        {
                            cmd.Parameters.Add(colParameter, pk, col.DataType);
                        }
                        else if(!col.AutoIncrement && String.IsNullOrEmpty(col.DefaultSetting))
                        {
                            if(col.DataType == DbType.Guid)
                            {
                                pk = Guid.NewGuid();
                            }
                            cmd.Parameters.Add(colParameter, pk, col.DataType);
                        }
                    }
                    else if(!col.AutoIncrement)
                    {
                        Control ctrl = tblEditor.FindControl(col.ColumnName);
                        object oVal = Utility.GetDefaultControlValue(col, ctrl, isAdd, true);
                        cmd.Parameters.Add(colParameter, oVal, col.DataType);
                    }
                }
            }

            //execute it
            DataService.ExecuteQuery(cmd);

            //save down any many/many bits
            if(!String.IsNullOrEmpty(_manyToManyMap))
            {
                string[] mmTables = _manyToManyMap.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries);

                foreach(string mmTableName in mmTables)
                {
                    SaveManyToMany(mmTableName);
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    private Control GetEditControl(TableSchema.TableColumn col)
    {
        Control cOut = null;
        //use special care with the Primary Key
        if (col.IsPrimaryKey && !col.IsForeignKey)
        {
            //don't want to edit an auto-increment
            if (col.AutoIncrement || col.DataType == DbType.Guid)
            {
                Label lblPK = new Label();
                lblPK.ID = "pkID";
                cOut = lblPK;
            }
            else
            {
                TextBox txtPK = new TextBox();
                txtPK.ID = "pkID";
                cOut = txtPK;
            }
        }
        else
        {
            string colName = col.ColumnName.ToLower();
            if (col.IsForeignKey)
            {
                DropDownList ddl = new DropDownList();
                string fkTableName = DataService.GetForeignKeyTableName(col.ColumnName, col.Table.Name, ProviderName);

                if (!String.IsNullOrEmpty(fkTableName))
                {
                    TableSchema.Table tblSchema = DataService.GetTableSchema(fkTableName, ProviderName,TableType.Table);
                    Query qry = new Query(tblSchema);

                    qry.OrderBy = OrderBy.Asc(tblSchema.Columns[1].ColumnName);

                    IDataReader rdr = qry.ExecuteReader();

                    //load up the dropdown
                    //by convention the descriptor should be the second field

                    if(col.IsNullable)
                    {
                        ListItem liNull = new ListItem("(Not Specified)", String.Empty);
                        ddl.Items.Add(liNull);
                    }

                    while(rdr.Read())
                    {
                        ListItem item = new ListItem(rdr[1].ToString(), rdr[0].ToString());
                        ddl.Items.Add(item);
                    }
                    rdr.Close();


                    cOut = ddl;
                }
            }
            else
            {
                switch (col.DataType)
                {
                    case DbType.Guid:
                    case DbType.AnsiString:
                    case DbType.String:
                    case DbType.StringFixedLength:
                    case DbType.Xml:
                    case DbType.Object:
                    case DbType.AnsiStringFixedLength:
                        if (Utility.IsMatch(colName, ReservedColumnName.CREATED_BY) || Utility.IsMatch(colName, ReservedColumnName.MODIFIED_BY))
                        {
                            cOut = new Label();
                        }
                        else
                        {
                            TextBox t = new TextBox();
                            if (Utility.GetEffectiveMaxLength(col)  > 250)
                            {
                                t.TextMode = TextBoxMode.MultiLine;
                                t.Height = Unit.Pixel(100);
                                t.Width = Unit.Pixel(500);
                            }
                            else
                            {
                                t.Width = Unit.Pixel(250);
                                if (colName.EndsWith("guid"))
                                {
                                    t.Text = Guid.NewGuid().ToString();
                                    t.Enabled = false;
                                }
                            }
                            cOut = t;
                        }
                        break;

                    case DbType.Binary:
                    case DbType.Byte:
                        //do nothing
                        break;
                    case DbType.Boolean:
                        CheckBox chk = new CheckBox();
                        if (Utility.IsMatch(colName, ReservedColumnName.IS_ACTIVE))
                        {
                            chk.Checked = true;
                        }
                        if (Utility.IsMatch(colName, ReservedColumnName.DELETED) || Utility.IsMatch(colName, ReservedColumnName.IS_DELETED))
                        {
                            chk.Checked = false;
                        }
                        cOut = chk;
                        break;

                    case DbType.Date:
                    case DbType.Time:
                    case DbType.DateTime:
                        if (Utility.IsMatch(colName, ReservedColumnName.MODIFIED_ON) || Utility.IsMatch(colName, ReservedColumnName.CREATED_ON))
                        {
                            cOut = new Label();
                        }
                        else
                        {
                            cOut = new CalendarControl();
                        }
                        break;

                    case DbType.Int16:
                    case DbType.Int32:
                    case DbType.UInt16:
                    case DbType.Int64:
                    case DbType.UInt32:
                    case DbType.UInt64:
                    case DbType.VarNumeric:
                    case DbType.Single:
                    case DbType.Currency:
                    case DbType.Decimal:
                    case DbType.Double:
                        TextBox tt = new TextBox();
                        tt.Width = Unit.Pixel(50);
                        //if (!this.isNew)
                        //tt.Text = this.GetColumnValue(col.ColumnName).ToString();
                        cOut = tt;
                        break;
                    default:
                        cOut = new TextBox();
                        break;
                }
            }
            if (cOut != null)
            {
                cOut.ID = col.ColumnName;
            }
        }
        if (cOut is TextBox)
        {
            TextBox tbx = (TextBox)cOut;
            ApplyCssClass(tbx, "scaffoldEditItem");
            if (cOut.GetType() == typeof(TextBox)) //Not Redundant! CalendarControl is TextBox == true; myCalendarControl.GetType() == typeof(TextBox) == false!
            {
                int maxLength = Utility.GetEffectiveMaxLength(col);
                if(maxLength > 0)
                {
                    tbx.MaxLength = maxLength;
                }
            }
        }
        return cOut;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static string BuildInsertSql(TableSchema.Table schema)
    {
        string sql = "INSERT INTO " + schema.Name;
        string colList = String.Empty;
        string paramList = String.Empty;

        foreach (TableSchema.TableColumn col in schema.Columns)
        {
            if (col.DataType != DbType.Binary && col.DataType != DbType.Byte)
            {
                if(col.IsPrimaryKey)
                {
                    if(!col.AutoIncrement && String.IsNullOrEmpty(col.DefaultSetting))
                    {
                        colList += col.ColumnName + ",";
                        paramList += Utility.PrefixParameter(col.ParameterName, schema.Provider) + ",";
                    }
                    
                }
                else if (!col.AutoIncrement)
                {
                    colList += col.ColumnName + ",";
                    paramList += Utility.PrefixParameter(col.ParameterName, schema.Provider) + ",";
                }
            }
        }

        colList = colList.Remove(colList.Length - 1, 1);
        paramList = paramList.Remove(paramList.Length - 1, 1);

        sql += "(" + colList + ") VALUES (" + paramList + ")";
        return sql;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static string BuildUpdateSql(TableSchema.Table schema)
    {
        string sql = "UPDATE " + schema.Name + " SET ";

        foreach (TableSchema.TableColumn col in schema.Columns)
        {
            if (col.DataType != DbType.Binary && col.DataType != DbType.Byte)
            {
                if (Utility.IsWritableColumn(col))
                {
                    sql += col.ColumnName + " = " + Utility.PrefixParameter(col.ParameterName, schema.Provider) + ",";
                }
            }
        }

        sql = sql.Remove(sql.Length - 1, 1);
        sql += " WHERE " + schema.PrimaryKey.ColumnName + " = " + Utility.PrefixParameter(schema.PrimaryKey.ColumnName, schema.Provider);
        return sql;
    }


    void lb_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        lblHeader.Text = lb.CommandArgument;
        SetTableName(lb.CommandName);

        Response.Redirect(Regex.Split(Request.Url.PathAndQuery, "\\?")[0]);
        //BindGrid(String.Empty);
        //BindGrid(lb.CommandName);
        //throw new Exception("The method or operation is not implemented.");
    }


    protected void ddlProviders_SelectedIndexChanged(object sender, EventArgs e)
    {
        ProviderName = ddlProviders.SelectedValue;
        BuildTableList();
        BindGrid(String.Empty);
    }

    #region Event Handlers
    protected void grid_Sorting(object sender, GridViewSortEventArgs e)
    {
        string columnName = e.SortExpression;
        //rebind the grid
        if (ViewState[SORT_DIRECTION] == null || ((string)ViewState[SORT_DIRECTION]) == SqlFragment.ASC)
        {
            ViewState[SORT_DIRECTION] = SqlFragment.DESC;
        }
        else
        {
            ViewState[SORT_DIRECTION] = SqlFragment.ASC;
        }
        BindGrid(columnName);
    }

    protected static void btnCancel_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.CurrentExecutionFilePath);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //try
        //{
            SaveEditor();
            ShowMessage("<font color=\"ForestGreen\"><b>Record Saved</b></font>");
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.CurrentExecutionFilePath);
        //}
        //catch (DbException x)
        //{
        //    ShowMessage("<font color=\"#990000\"><b>" + x.Message + "</b></font>");
        //}
    }


    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string pk = null;
        Control c = tblEditor.FindControl("pkID");
        if (c is Label)
        {
            pk = ((Label)c).Text;
        }
        else if (c is TextBox)
        {
            pk = ((TextBox)c).Text;
        }

        if (pk != null)
        {
            Query qry = new Query(Schema);
            qry.AddWhere(Schema.PrimaryKey.ColumnName, pk);
            DataService.ExecuteQuery(qry.BuildDeleteCommand());
        }
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.CurrentExecutionFilePath);
    }

    private void ShowMessage(string message)
    {
        Label lblMessage = (Label)tblEditor.FindControl("lblMessage");
        if (lblMessage != null)
        {
            lblMessage.Text = message + " <br><i>" + DateTime.Now + "</i>";
        }
    }

    protected static void btnAdd_Click(object sender, EventArgs e)
    {
        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.CurrentExecutionFilePath + "?id=0");
    }
    #endregion

    protected void grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grid.PageIndex = e.NewPageIndex;
        BindGrid(String.Empty);
    }

}

public sealed class TextComparer : IComparer
{
    public int Compare(object x, object y)
    {
      return x.ToString().CompareTo(y.ToString());
    }
}
