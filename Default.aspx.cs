using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing;



public partial class Default2 :  System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
       // פופ אפ לבדיקה אם רוצים למחוק את המשחק
        popUpCheck.Visible = false;
        backWindow.Visible = false;

        // פונקציה לבדיקת מצב פיבלוש
        checkGamePublish();
        //משתנה למצב עריכת שאלה
        Session["editQ"] = "0";
    }

    //פונקציה לשינוי הצ'ק בוקס בהתאם למצב המשחק - אם יש מעל 10 שאלות הצ'קבוקס יכול להיות פעיל, כלומר המשחק יכול להיות מפורסם
    protected void checkGamePublish()
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        foreach (GridViewRow row in GridView1.Rows)
        {
            //חיפוש הלייבל שבו מופיע ה ID של המשחק
            Label gameCodeLbl = (Label)row.FindControl("gamCode");
            //בעזרת האי-די של המשחק נוכל לבדוק האם עומד בתנאי הפרסום
            string GameCode = gameCodeLbl.Text;

            //דוגמה לבדיקה - אם קיימים לפחות 10 שאלות
            XmlNodeList quest = xmlDoc.SelectNodes("/pullTheCarrot/game[@GameCode='" + GameCode + "']/questions/question");
           //חיפוש מספר השאלות בגרידויו כדי לבדוק אם מתחת ל10
            Label QuestColor = (Label)row.FindControl("numQuestion");


            //חיפוש הצ'אק-בוקס על פי האי-די שלו
            CheckBox GameIsPublishCb = (CheckBox)row.FindControl("isPublish");
            Label ToolTip = (Label)row.FindControl("tooltiptxt");

            if (quest.Count >= 10)
            {
                GameIsPublishCb.Enabled = true;
                ToolTip.CssClass = "tooltipTxtNotActive";


            }
            else
            {
                GameIsPublishCb.Enabled = false;
                //אם מקודם המשחק היה מפורסם, אנחנו רוצים להחזיר אותו ללא מפורסם בעץ
                XmlNode IsPublish = xmlDoc.SelectSingleNode("/pullTheCarrot/game[@GameCode='" + GameCode + "']");
                IsPublish.Attributes["isPublish"].InnerText = "False";
                XmlDataSource1.Save();

                //וגם לשנות את הפקד עצמו ללא לחוץ
                GameIsPublishCb.Checked = false;
                //לצבוע את מספר השאלות שמתחת ל10 באדום
                QuestColor.ForeColor = System.Drawing.Color.Red;
                //מעבר עכבר על צ'קבוקס
                ToolTip.CssClass = "tooltipTxt";

            }

        }
    }

    protected void addGame_Click(object sender, EventArgs e)
    {
        // איפוס קוד משחק ביצירת משחק חדש 

        Session["newGame"] = "0";

        
        // מעבר לעמוד הגדרות
        Response.Redirect("settings.aspx");
    }


    protected void isPublish_CheckedChanged(object sender, EventArgs e)
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();

        // תחילה אנו בודקים מהו הקוד של המשחק הזה בעץ 
        CheckBox myCheckBox = (CheckBox)sender;

        // מושכים את הקוד באמצעות המאפיין שהוספנו באופן ידני לתיבה
        string theCode = myCheckBox.Attributes["itemID"];


        //שאילתא למציאת השאלה שברצוננו לעדכן
        XmlNode theGame = xmlDoc.SelectSingleNode("/pullTheCarrot/game[@GameCode=" + theCode + "]");

        //קבלת הערך החדש של התיבה לאחר הלחיצה
        bool NewIsPablish = myCheckBox.Checked;

        //עדכון של המאפיין בעץ
        theGame.Attributes["isPublish"].InnerText = NewIsPablish.ToString();

        //שמירה בעץ והצגה
        XmlDataSource1.Save();
        GridView1.DataBind();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו הקוד של המשחק בעץ
        ImageButton myEdit = (ImageButton)e.CommandSource;
        
        // אנו מושכים את קוד המשחק  באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theCode = myEdit.Attributes["itemID"];
        Session["gameCodeSession"] = theCode;

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deleteRow":
                deleteRow();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "editRow":

                Response.Redirect("Edit.aspx");
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה                    
            case "setRow":

                // משתנה לבדיקה
                Session["newGame"] = "1";
               
                // מעבר לעמוד עריכה
                Response.Redirect("settings.aspx");
                break;
        }
    }

    protected void deleteRow()
    {
        popUpCheck.Visible = true;
        backWindow.Visible = true;

        string gameToDelete = Session["gameCodeSession"].ToString();

        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode node = Document.SelectSingleNode("//game[@GameCode='" + gameToDelete + "']/GameSubject");

      string GameNameDelete = "האם אתה בטוח שברצונך למחוק את המשחק" +" " +Server.UrlDecode(node.InnerXml) + "?";
        checkDelite.Text = GameNameDelete ;
        
    }



    protected void noDelite_Click(object sender, EventArgs e)
    {
        popUpCheck.Visible = false;

    }

    protected void yesDelite_Click(object sender, EventArgs e)
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי הקוד שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        string gameToDelete = Session["gameCodeSession"].ToString();
       

        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode node = Document.SelectSingleNode("/pullTheCarrot/game[@GameCode='" + gameToDelete + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView1.DataBind();
    }
}