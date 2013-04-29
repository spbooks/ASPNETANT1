using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace chapter_02_core_libraries
{
    public partial class Iterators : System.Web.UI.Page
    {
        public TextWriter CreateXmlStream()
        {
            return new StringWriter(new StringBuilder());
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<h2>Old Way</h2>");
            OldWay();
            Response.Write("<hr />");
            Response.Write("<h2>Using Iterators</h2>");
            NewWay();
        }

        public void OldWay()
        {
            //Loads all posts from the database into memory.
            ICollection<BlogPost> allPosts = GetAllDataBatch();

            using (TextWriter writer = CreateXmlStream())
            {
                foreach (BlogPost post in allPosts)
                {
                    SerializeBlogPost(post, writer);
                    writer.Flush();
                }
            }
        }

        public void NewWay()
        {
            BatchIterator<BlogPost> batches = new BatchIterator<BlogPost>(GetPostBatch);

            using (TextWriter writer = CreateXmlStream())
            {
                foreach (ICollection batch in batches)
                {
                    foreach (BlogPost post in batch)
                    {
                        SerializeBlogPost(post, writer);
                    }
                    writer.Flush();
                }
            }
        }

        public void SerializeBlogPost(BlogPost post, TextWriter writer)
        {
            //Simulater writing to the text writer.
            Response.Write(HttpUtility.HtmlEncode("<BlogPost>" + post.Title + "</BlogPost>"));
            Response.Write("<br />");
        }

        public void FlushToFile(TextWriter writer)
        {
            //writer.Flush();
            Response.Write("<strong>Contents flushed to the file.</strong><br />");
        }

        void OpenFileForWriting()
        {
            Response.Write("<strong>Xml file opened for writing.</strong><br />");
        }

        void CloseFile()
        {
            Response.Write("<strong>Xml file closed.</strong><br />");
        }

        static ICollection<BlogPost> GetPostBatch(int index)
        {
            List<BlogPost> posts = new List<BlogPost>();
            // code to simulate loading posts from the database...
            switch (index)
            {
                case 0:
                    posts.AddRange(new BlogPost[]
                                       {
                                           new BlogPost("First Post")
                                           , new BlogPost("Second Post")
                                           , new BlogPost("Third Post")
                                       });
                    break;

                case 1:
                    posts.AddRange(new BlogPost[]
                                       {
                                           new BlogPost("Fourth Post")
                                           , new BlogPost("Fifth Post")
                                           , new BlogPost("Sixth Post")
                                       });
                    break;

                case 2:
                    posts.AddRange(new BlogPost[]
                                       {
                                           new BlogPost("Seveth Post")
                                           , new BlogPost("Eighth Post")
                                       });
                    break;

                case 3:
                    //no more data to return.
                    return null;
            }
            return posts;
        }

        static ICollection<BlogPost> GetAllDataBatch()
        {
            List<BlogPost> posts = new List<BlogPost>();
            // code to simulate loading posts from the database...
            posts.AddRange(new BlogPost[]
       {
           new BlogPost("First Post")
           , new BlogPost("Second Post")
           , new BlogPost("Third Post")
       
           , new BlogPost("Fourth Post")
           , new BlogPost("Fifth Post")
           , new BlogPost("Sixth Post")
           , new BlogPost("Seveth Post")
           , new BlogPost("Eighth Post")
       });
            return posts;
        }
    }

    public class BlogPost
    {
        public BlogPost(string title)
        {
            this.title = title;
        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        private string title;
    }
}
