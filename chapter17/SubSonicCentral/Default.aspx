<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Theme="Default" MasterPageFile="~/res/MasterPage.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="cphLeft" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphRight" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphFull" Runat="Server">
<h3>Welcome to SubSonic 2.0!</h3>
    <p>
        What is SubSonic? Well, simply put it's an effort by a group of .NET geeks
        to bring Ruby On Rails simplicity to .NET. We're starting with the Database and
        are slowly moving our way outward to web applications in general. This is done with
        a combination of OR/Mapping, code generation, Unit Testing, and adherence to convention
        (over configuration).</p>
        Don't take this the wrong way - we're NOT OR/M nuts. We fully support and in fact 
        want you to use SPs and Views - but when you need to, not because you have to. The choice
        is yours, SubSonic will wrap these things for you and off you go!
    <p>
        A lot has been made of Rails, and much of the hype is deserved. However the fun
        in Rails is not necessarily Rails itself - it's the joy if being able to do the
        work you love without the tedium of doing the same thing over and over (like creating
        a Data Access Layer).</p>
    <p>
        It's our goal to bring this simplicity to you. We're already using it on the Commerce
        Starter Kit for ASP.NET 2.0. It's all yours - free!</p>
        
 <h3>What's New in 2.0?</h3>
    <p>
        With version 2.0 we've addressed some major requests from our community, and tried
        to add in functionality to make your day that much easier. Here's what's new:</p>
    <p>
        <strong>Multiple Database support!</strong> Now you can generate a full DAL for
        as many databases as you like.</p>
    <p>
        <strong>Enterprise Library 3.0 Support</strong>. Just added this in and it works
        nicely.</p>
    <p>
        <strong>All-new command-line tool.</strong> You can now use our command-line tool
        (called SubCommander) to do all kinds of fun things, like generate your code and
        version your database. You can even hook into it using Visual Studio's External
        Tools - this will allow you to generate your project's code with the click of a
        button, and it will use your project settings (look for a blog post on Rob's blog).</p>
    <p>
        <strong>Improved querying</strong>. You can now use our Query tool to run OR's,
        IN's, and aggregates. You can even type what you want to see:<br />
        <span style="color: #a65300">IDataReader</span> <span style="color: maroon">rdr</span>
        = <span style="color: navy">new</span> <span style="color: maroon">SubSonic</span>.<span
            style="color: #a65300">Query</span>(<span style="background: #ffffe6">"Products"</span>).<span
                style="color: maroon">WHERE</span>(<span style="background: #ffffe6">"CategoryID = 5"</span>).<span
                    style="color: maroon">ExecuteReader</span>();<br />
        We've also renamed many of our methods (well, we've added aliases) to make the query
        more readable. You can now use WHERE, AND, OR, IN, BETWEEN_AND, etc. to make your
        calls that much more readable.</p>
    <!--EndFragment-->
    <p>
        <strong>New Controls</strong>. You can now use our Smart Dropdown, which loads itself:<br />
        &lt;subsonic:DropDown id=mySmarty runat=server tablename="categories" /&gt;<br />
        <br />
        You can also use our new ManyToMany Checkbox list helper to both list and save information
        for many to many relationships:<br />
        &lt;subsonic: ManyManyList id=myList runat=server MapTableName="Product_Category_Map"
        PrimaryTableName="Products" PrimaryKeyValue="1" ForeignTableName="Categories" /&gt;</p>
    <p>
        <strong>A new AutoScaffold page</strong> that you can drop right into your project
        to admin all your tables. This thing reads your tables and creates scaffolds for
        you automagically (thanks Eric!).</p>
    <p>
        <strong>A completely reworked code-generation system</strong> that uses an ASP-style
        templating system. You can now code up your templates like you would an ASP page
        (also just like CodeSmith). Test them in your web site to make sure they work, then
        replace (or add to) the bits that get generated at runtime. You can override our
        templates by specifying a template directory in the web.config:<br />
        <!--StartFragment-->
        &nbsp;<span style="color: blue">&lt;</span><span style="color: #a31515">SubSonicService</span><span
            style="color: blue"> </span><span style="color: red">defaultProvider</span><span
                style="color: blue">=</span>"<span style="color: blue">Northwind</span>"<span style="color: blue">
                </span><span style="color: red">fixPluralClassNames</span><span style="color: blue">=</span>"<span
                    style="color: blue">false</span>"<span style="color: blue"> </span><span style="color: red">
                        templateDirectory</span><span style="color: blue">=</span>"<span style="color: blue">D:\PathToMyTemplates</span>"<span
                            style="color: blue">&gt;</span></p>
    <p>
        <!--EndFragment-->
        <strong>Regular Expression Naming Engine.</strong> If you don't like what our convention
        is, then you can use your own with some simple regex. Pass a simple string, or a
        dictionary replacement and all of your tables/views will be renamed as you specify.</p>
    <p>
        <strong>Query Inspection</strong>. Want to know what's happening to your query,
        and how long it's taking? You can simply use the new Inspect() method which outputs
        the results and statistics to HTML for you to review.</p>
    <p>
        <strong>Improved Trace/Debug</strong>. We've added tracing to (almost) every facet
        of SubSonic, so if you turn tracing on you can see what SubSonic's trying to do.
        We're always adding to this and if you see something we've missed, let us know :).<br />
    </p>
    <p>
        &nbsp;</p>
</asp:Content>