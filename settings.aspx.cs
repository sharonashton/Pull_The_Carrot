using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


public partial class settings : System.Web.UI.Page
{
    XmlDocument myDoc = new XmlDocument();

    protected void Page_init(object sender, EventArgs e)
    {
        // הזנת הנתונים הקיימים במשחק קיים
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));     
      
        // בדיקה של יצירת משחק חדש
        string myCheck = Session["newGame"].ToString();

        if (myCheck == "1")
        {
            string gameCode = Session["gameCodeSession"].ToString();

            // שם המשחק
            XmlNode gameName = myDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/GameSubject");
            addGameTB.Text = Server.UrlDecode(gameName.InnerXml);

            // זמן המשחק
            XmlNode myGameTime = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + gameCode + "]/ @timePerQuest");
            gameTime.SelectedValue = myGameTime.Value; 

            // מצב מבחן או תרגיל
            XmlNode myScore = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + gameCode + "]/ @GameScore");
            ScoreGame.SelectedValue = myScore.Value;
          

                // פידבק סופי
            XmlNode gameFeedback = myDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/endFeedback");
            endFeedback.Text = Server.UrlDecode(gameFeedback.InnerXml);
        }
        else
        {
            addGameTB.Text = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
 

    }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void creatGame_Click(object sender, EventArgs e)
    {
        // טעינת העץ
        myDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        // בדיקה של יצירת משחק חדש
        string myCheck = Session["newGame"].ToString();

        if (myCheck == "1")
        {
            string gameCode = Session["gameCodeSession"].ToString();
           
            // עריכת שם המשחק
            XmlNode changeName = myDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/GameSubject");
            changeName.InnerXml = addGameTB.Text;

            // עריכת זמן המשחק
            string timeChoosh = ((RadioButtonList)FindControl("gameTime")).SelectedValue;

            XmlNode timeCheng = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + gameCode + "][ @timePerQuest]");
            timeCheng.Attributes["timePerQuest"].InnerText = timeChoosh;

            //  עריכת תרגול או מבחן בכפתור הרדיו
            string scoreChoosh = ((RadioButtonList)FindControl("ScoreGame")).SelectedValue;

            XmlNode scoreCheng = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + gameCode + "][ @GameScore]");
            scoreCheng.Attributes["GameScore"].InnerText = scoreChoosh;

            // עריכת פידבק סופי
            XmlNode feedbackeName = myDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/endFeedback");
            //feedbackeName.InnerXml = "כל הכבוד! סיימת את יום העבודה שלך והבאת את כל הגזרים הביתה";
            feedbackeName.InnerXml = endFeedback.Text;

            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));
           
        }
        else
        {
            // מציאת מספר המשחק הגדול ביותר והגדלתו ב-1
            int Maxcode = Convert.ToInt16(myDoc.SelectSingleNode("//game[not(@GameCode < //game/@GameCode)]/@GameCode").Value);
            Maxcode++;

            string theCode = Maxcode.ToString();
            Session["gameCodeSession"] = theCode;


            // יצירת ענף משחק חדש     
            XmlElement newGame = myDoc.CreateElement("game");
            newGame.SetAttribute("isPublish", "false");
            newGame.SetAttribute("timePerQuest", "60");
            newGame.SetAttribute("GameCode", Maxcode.ToString());
            newGame.SetAttribute("GameScore", "1");

            // יצירת ענף שם המשחק
            XmlElement gameName = myDoc.CreateElement("GameSubject");
            gameName.InnerXml = Server.UrlEncode(addGameTB.Text);
            newGame.AppendChild(gameName);

            // יצירת ענף שאלות ללא שאלות עצמם
            XmlElement myquestions = myDoc.CreateElement("questions");
            newGame.AppendChild(myquestions);


            // הוספת ענף המשחק לעץ כמשחק ראשון
            XmlNode FirstGame = myDoc.SelectNodes("/pullTheCarrot/game").Item(0);
            myDoc.SelectSingleNode("/pullTheCarrot").InsertBefore(newGame, FirstGame);

            //שמירת שינויים בעץ
            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            addGameTB.Text = "";

            //  בחירת הזמנים בכפתור הרדיו
            string timeChoosh = ((RadioButtonList)FindControl("gameTime")).SelectedValue;

            XmlNode timeCheng = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + Maxcode.ToString() + "][ @timePerQuest]");
            timeCheng.Attributes["timePerQuest"].InnerText = timeChoosh;

            //  בחיר תרגול או מבחן בכפתור הרדיו
            string scoreChoosh = ((RadioButtonList)FindControl("ScoreGame")).SelectedValue;

            XmlNode scoreCheng = myDoc.SelectSingleNode("/pullTheCarrot /game [@GameCode = " + Maxcode.ToString() + "][ @GameScore]");
            scoreCheng.Attributes["GameScore"].InnerText = scoreChoosh;

            //יצירת פידבק סיום
            string myFeedback = " ";
            if (endFeedback.Text != " ")
            {
                myFeedback = endFeedback.Text;

            }
            //יצירת פידבק סיום
            XmlElement Feedback = myDoc.CreateElement("endFeedback");
            Feedback.InnerXml = Server.UrlEncode(myFeedback);
            newGame.AppendChild(Feedback);

            //שמירת שינויים בעץ
            myDoc.Save(Server.MapPath("trees/XMLFile.xml"));
        }

           

        // מעבר לדדף עריכה
        Response.Redirect("Edit.aspx");

    }
    

    //כפתור חזרה למסך הראשי
        protected void goBack_Click(object sender, EventArgs e)
    {
        Session["newGame"] = "1";

        Response.Redirect("Default.aspx");
    }
}