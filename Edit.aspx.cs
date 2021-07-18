using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing;
using System.Xml;


public partial class Edit : System.Web.UI.Page
{
    //נתיב לשמירה התמונות
    string imagesLibPath = "uploadedFiles/";


    protected void Page_init(object sender, EventArgs e)
    {


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string gamCodeS = Session["gameCodeSession"].ToString(); ///הסשן של קוד המשחק הנוכחי

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));//קריאה לעץ

        XmlNode mygameNode = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/GameSubject");//הזרקת נושא המשחק הנוכחי לתוך משתנה

        gameNameTXT.Text = Server.UrlDecode(mygameNode.InnerXml);//נושא המשחק

        // יצירת משתנים שלא מתאפסים בכל לחיצת כפתור
        if (IsPostBack == false)
        {
            int ansNum = 2;//ספירת מספר מסיחים
            Session["myansNum"] = ansNum;
            Session["myType"] = "";            //משתנה ליצירת סוג משתנה                      
            cancelEdit.Visible = false;///העלמת כפתור ביטול עריכה
            //PanelQuestionIMGEdit.CssClass = "QpanelOff";
            Session["QpicName"] = " ";
        }

        string GameCode = Session["GameCodeSession"].ToString();//קבלת המשחק שממנו הגענו לעמוד העריכה      
        XmlNodeList questionNum = xmlDoc.SelectNodes("//game[@GameCode='" + gamCodeS + "']/questions/question");
        questionCount.Text = Server.UrlDecode((questionNum.Count).ToString());// הצגת מספר שאלות
        Session["myquestionNum"] = questionNum.Count;//ספירת מספר שאלות

        //כאן נרשום את השאילתה החדשה לתוך רכיב ה- XmlDataSource
        //על מנת שהשאלות יהיו מותאמות למשחק שבחרנו
        XmlDataSource1.XPath = "/pullTheCarrot/game[@GameCode=" + GameCode + "]/questions/question";
        //הסתרת חלונית אזהרת מחיקה עד להגדרה אחרת
        popUpCheck.Visible = false;
        backWindow.Visible = false;
        //הסתרת חלונית רקע להגדלת תמונה עד להגדרה אחרת
        BigPic.Visible = false;
        //הסתרת המסיחים הנוספים עד להגדרה אחרת
        answer3.Visible = false;
        answer4.Visible = false;
        answer5.Visible = false;
        //קריאה לפונקציה להוספת מסיחים
        addAnswerCall();
    }


    /// <summary>
    /// פונקציה לבדיקת האם הועלתה תמונה לשרת
    /// </summary>
    protected void CheckImgQ()
    {
        Session["picQ"] = ""; //יצירת סשן לתמונה בשאלה, כרגע ריק
        string fileType = ((FileUpload)FindControl("FileUpload0")).PostedFile.ContentType;  // משתנה לקליטת סוג קובץ מסוג תמונה
        string imageNewName; // משתנה ליצירת שם חדש לתמונה
        Page.Response.Write("<script>console.log(' enters loop, savinf pic ');</script>");// בדיקה האם התמונה נשמרת

        if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
        {
            Session["picQ"] = "img";///במידה וקיימת תמונה נשנה את סוג המסיח ל Img
            string fileName = ((FileUpload)FindControl("FileUpload0")).PostedFile.FileName;// הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));  // הסיומת של הקובץ
            string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss_ffff");    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
            imageNewName = myTime + "0" + endOfFileName;// חיבור השם החדש עם הסיומת של הקובץ
            ((FileUpload)FindControl("FileUpload0")).PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);//שמירה של הקובץ לספרייה בשם החדש שלו
            ((ImageButton)FindControl("imagA0")).ImageUrl = imagesLibPath + imageNewName;//הצגה של הקובץ החדש מהספרייה
            Session["QpicName"] = imageNewName; // שמירת השם החדש של התמונה לתוך הסשן לתמונה בשאלה.

            // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
            System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload0")).PostedFile.InputStream);
            System.Drawing.Image objImage = FixedSize(bmpPostedImage, 256, 256);// שינוי הגודל לתמונה
            objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);//שמירת שם התמונה 
        }
        else // אם הקובץ אינו תמונה
        {
            Session["picQ"] = "text";//במידה ואין תמונה נשנה את סוג המסיח לטקסט
            imageNewName = "*";// הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
        }
    }
    protected void CheckImg2() // פונקציה הבודקת את המסיחים - האם הועלתה אליהם תמונה או לא
    {
        int ansNum = Convert.ToInt16(Session["myansNum"]); // משתנה למספר המסיח
        string[] myarray = new string[ansNum];////מערך לקליטת סוג המסיח
        string[] myImgName = new string[ansNum];// מערך לקליטת שם התמונה

        //לולאה שעוברת על כל התמונות שהועלו ומוסיפה אותם לתיקיית הפרויקט
        //ניתן לעשות גם קוד כפול נפרד עם השמות של הפקדים ללא צורך בשימוש ב FindControl
        for (int i = 1; i <= ansNum; i++)
        {
            string fileType = ((FileUpload)FindControl("FileUpload" + i)).PostedFile.ContentType; // הלולאה עוברת על כל המסיחים ויוצרת משתנים לקליטת סוג קובץ מסוג תמונה
            string imageNewName;//משתנה לצורך קליטת שם התמונות במסיחים
            Page.Response.Write("<script>console.log(' enters loop, savinf pic ');</script>");

            if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
            {
                Session["myType"] = "img";///במידה וקיימת תמונה נשנה את סוג המסיח ל Img
                string fileName = ((FileUpload)FindControl("FileUpload" + i)).PostedFile.FileName;// הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));  // הסיומת של הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss_ffff");    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
                imageNewName = myTime + i + endOfFileName;// חיבור השם החדש עם הסיומת של הקובץ
                ((FileUpload)FindControl("FileUpload" + i)).PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);//שמירה של הקובץ לספרייה בשם החדש שלו
                ((ImageButton)FindControl("imagA" + i)).ImageUrl = imagesLibPath + imageNewName;//הצגה של הקובץ החדש מהספרייה
                // Bitmap המרת הקובץ שיתקבל למשתנה מסוג
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(((FileUpload)FindControl("FileUpload" + i)).PostedFile.InputStream);
                System.Drawing.Image objImage = FixedSize(bmpPostedImage, 256, 256);
                objImage.Save(Server.MapPath(imagesLibPath) + imageNewName);
            }
            else
            {
                Session["myType"] = "text";//במידה ואין תמונה נשנה את סוג המסיח לטקסט
                imageNewName = "*";// הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
            }
            string myType = Session["myType"].ToString();
            myarray[i - 1] = myType;///הזרקה של סוג המסיח למערך
            myImgName[i - 1] = imageNewName.ToString(); ///הזרקה של שם התמונה למערך
            Page.Response.Write("<script>console.log('" + myImgName[i - 1] + "');</script>");
        }
        Session["myArr"] = myarray;
        Session["myimgName"] = myImgName;
    }
    //פונקציה המתרחשת בפוסטבק
   
    //פונקציה לשמירת שאלה
    protected void saveQ_Click(object sender, EventArgs e)
    {

        string checkEditQ = Session["editQ"].ToString();//שליפה של משתנה למצב שמירה- האם הוא בעריכה או ביצירה של שאלה חדשה 

        XmlDocument xmlDoc = new XmlDocument(); // טעינה של העץ
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        string gamCodeS = Session["gameCodeSession"].ToString();// הקוד של המשחק       
        int ansNum = Convert.ToInt16(Session["myansNum"]);// ספירת מספר מסיחים     

        cancelEdit.Visible = false;///העלמת כפתור ביטול עריכה
        CheckImg2();
        CheckImgQ();

        // נכנס למצב של יצירת שאלה חדשה
        if (checkEditQ == "0")
        {
            int questionNum = Convert.ToInt16(Session["myquestionNum"]);//משתנה לספירת שאלות
            questionNum++;
            Session["myquestionNum"] = questionNum;

            XmlElement newQuestion = xmlDoc.CreateElement("question");//// יצירת ענף שאלות 
            newQuestion.SetAttribute("id", questionNum.ToString());
            XmlElement QuestionTXT = xmlDoc.CreateElement("questionText");////יצירת ענף של טקסט השאלה
            QuestionTXT.InnerXml = Server.UrlEncode(theQuestion.Text);
            newQuestion.AppendChild(QuestionTXT);

            XmlElement imgQ = xmlDoc.CreateElement("img"); //יצירת תמונה
            newQuestion.AppendChild(imgQ);
            string QpicName = Session["QpicName"].ToString();
            string picQ = Session["picQ"].ToString();
            if (picQ == "img")
            {
                imgQ.InnerXml = Server.UrlEncode(QpicName);
               // PanelQuestionIMGEdit.Visible = true;
            }
            else
            {
                imgQ.InnerXml = Server.UrlEncode("empty");
            }

            XmlElement answers = xmlDoc.CreateElement("answers");//יצירת ענף לתשובות
            newQuestion.AppendChild(answers);

            string[] imgName = (string[])Session["myimgName"]; ////מערך של שמןת התמונות
            string[] myarray = (string[])Session["myArr"];

            XmlElement answer1 = xmlDoc.CreateElement("answer");

            if (myarray[0] == "text")///בדיקה אם סוג המסיח טקסט 
            {
                answer1.InnerXml = Server.UrlEncode(rightAnswer.Text); /////תשלוף את הנתונים מתוך הטקסטבוקס
            }

            if (myarray[0] == "img")    ///בדיקה אם סוג המסיח הוא תמונה
            {
                answer1.InnerXml = Server.UrlEncode(imgName[0]);///תשלוף את הנתונים מתוך מערך של שמות התמונות
            }
            /// מזין את סוג המסיח לאטריביוט
            answer1.SetAttribute("AnsType", myarray[0]);
            answer1.SetAttribute("feedback", "true");
            answer1.SetAttribute("ansID", "1");
            answers.AppendChild(answer1);


            XmlElement answer2 = xmlDoc.CreateElement("answer");
            /// כל התהליך חוזר על עצמו על המסיח השני וכן הלאה
            if (myarray[1] == "text")
            {
                answer2.InnerXml = Server.UrlEncode(wrongAnswer.Text);
            }
            if (myarray[1] == "img")
            {
                answer2.InnerXml = Server.UrlEncode(imgName[1]);
            }

            answer2.SetAttribute("AnsType", myarray[1]);
            answer2.SetAttribute("feedback", "false");
            answer2.SetAttribute("ansID", "2");
            answers.AppendChild(answer2);


            if (ansNum >= 3)//הוספת תוכן מסיחים לעץ - מסיחים מעל 3
            {
                XmlElement answer3 = xmlDoc.CreateElement("answer");

                if (myarray[2] == "text")
                {
                    answer3.InnerXml = Server.UrlEncode(wrongAnswer2.Text);
                }
                if (myarray[2] == "img")
                {
                    answer3.InnerXml = Server.UrlEncode(imgName[2]);
                }

                answer3.SetAttribute("AnsType", myarray[2]);
                answer3.SetAttribute("feedback", "false");
                answer3.SetAttribute("ansID", "3");
                answers.AppendChild(answer3);
            }

            if (ansNum >= 4)
            {
                XmlElement answer4 = xmlDoc.CreateElement("answer");
                if (myarray[3] == "text")
                {
                    answer4.InnerXml = Server.UrlEncode(wrongAnswer3.Text);

                }
                if (myarray[3] == "img")
                {
                    answer4.InnerXml = Server.UrlEncode(imgName[3]);
                }

                answer4.SetAttribute("AnsType", myarray[3]);
                answer4.SetAttribute("feedback", "false");
                answer4.SetAttribute("ansID", "4");
                answers.AppendChild(answer4);
            }

            if (ansNum >= 5)
            {
                XmlElement answer5 = xmlDoc.CreateElement("answer");
                if (myarray[4] == "text")
                {
                    answer5.InnerXml = Server.UrlEncode(wrongAnswer4.Text);
                }
                if (myarray[4] == "img")
                {
                    answer5.InnerXml = Server.UrlEncode(imgName[4]);
                }
                answer5.SetAttribute("AnsType", myarray[4]);
                answer5.SetAttribute("feedback", "false");
                answer5.SetAttribute("ansID", "5");
                answers.AppendChild(answer5);
            }

            ////  הוספת ענף השאלות לעץ ושמירה
            xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions").AppendChild(newQuestion);
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            //ספירת מספר שאלות
            XmlNodeList MYquestionNum = xmlDoc.SelectNodes("//game[@GameCode='" + gamCodeS + "']/questions/question");

            // הצגת מספר שאלות
            questionCount.Text = Server.UrlDecode((MYquestionNum.Count).ToString());
            Session["myquestionNum"] = MYquestionNum.Count;
        }
        // נכנס למצב של עריכת שאלה קיימת
        else
        {
            // מספר של השאלה שעורכים
            string questionID = Session["questionID"].ToString();
            // עריכת השאלה
            XmlNode changeQustion = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/questionText");
            XmlNode imgQ = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/img");
            changeQustion.InnerXml = theQuestion.Text;

            string QpicName = Session["QpicName"].ToString();
            string picQ = Session["picQ"].ToString();
            if (picQ == "img")
            {
                imgQ.InnerXml = Server.UrlEncode(QpicName);
                PanelQuestionIMGEdit.Visible = true;
            }
            else
            {
                imgQ.InnerXml = Server.UrlEncode("empty");
            }

            // עריכת מסיחים
            XmlNode answer1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='1']");
            XmlNode answer1type = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='1']/@AnsType");

            ////מערך של שמות התמונות
            //string[] imgName = (string[])Session["imgName"]; //erased
            string[] imgName = (string[])Session["myimgName"];
            if (answer1type.InnerXml == "text")
            {
                answer1.InnerXml = rightAnswer.Text;
            }
            if (answer1type.InnerXml == "img")
            {
                //answer1.InnerXml = imgName[0];
                answer1.InnerXml = Server.UrlEncode(imgName[0]);
            }

            XmlNode answer2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='2']");
            XmlNode answer2type = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='2']/@AnsType");

            if (answer2type.InnerXml == "text")
            {
                answer2.InnerXml = wrongAnswer.Text;
            }
            if (answer2type.InnerXml == "img")
            {
                answer2.InnerXml = imgName[1];
            }

            // הוספת מסיחים מעל 3
            if (ansNum >= 3)
            {
                // ספירת מספר מסיחים
                XmlNodeList myAnwers = xmlDoc.SelectNodes("//game[@GameCode='" + gamCodeS + "']/questions/question[@id ='" + questionID + "']/answers/answer");

                // אם לא קיים 3 ענפים למסיחים הוסיף ענף
                if (myAnwers.Count == 2)
                {
                    XmlNode answers = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers");

                    XmlElement answer3 = xmlDoc.CreateElement("answer");
                    answer3.InnerXml = Server.UrlEncode(wrongAnswer2.Text);
                    answer3.SetAttribute("AnsType", "text");  //צריך לבדוק איך משנים בין טקסט לתמונה בסוג מסיח
                    answer3.SetAttribute("feedback", "false");
                    answer3.SetAttribute("ansID", "3");
                    answers.AppendChild(answer3);
                }
                //עריכת מסיח
                XmlNode answeredit3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='3']");
                XmlNode answer3type = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='3']/@AnsType");

                if (answer3type.InnerXml == "text")
                {
                    answeredit3.InnerXml = wrongAnswer2.Text;
                }
                if (answer3type.InnerXml == "img")
                {
                    answeredit3.InnerXml = imgName[2];
                }
            }

            if (ansNum >= 4)
            {
                // ספירת מספר מסיחים
                XmlNodeList myAnwers = xmlDoc.SelectNodes("//game[@GameCode='" + gamCodeS + "']/questions/question[@id ='" + questionID + "']/answers/answer");
                Page.Response.Write("<script>console.log('" + myAnwers.Count + "');</script>");

                // אם לא קיים 4 ענפים למסיחים הוסיף ענף

                if (myAnwers.Count == 3)
                {
                    XmlNode answers = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers");

                    XmlElement answer3 = xmlDoc.CreateElement("answer");
                    answer3.InnerXml = Server.UrlEncode(wrongAnswer3.Text);
                    answer3.SetAttribute("AnsType", "text");  //צריך לבדוק איך משנים בין טקסט לתמונה בסוג מסיח
                    answer3.SetAttribute("feedback", "false");
                    answer3.SetAttribute("ansID", "4");
                    answers.AppendChild(answer3);
                }
                // עריכת מסיח
                XmlNode answer4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='4']");
                XmlNode answer4type = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='4']/@AnsType");

                if (answer4type.InnerXml == "text")
                {
                    answer4.InnerXml = wrongAnswer3.Text;
                }
                if (answer4type.InnerXml == "img")
                {
                    answer4.InnerXml = imgName[3];
                }
            }

            if (ansNum >= 5)
            {
                // ספירת מספר מסיחים
                XmlNodeList myAnwers = xmlDoc.SelectNodes("//game[@GameCode='" + gamCodeS + "']/questions/question[@id ='" + questionID + "']/answers/answer");

                // אם לא קיים 5 ענפים למסיחים הוסיף ענף
                if (myAnwers.Count == 4)
                {
                    XmlNode answers = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers");

                    XmlElement answer3 = xmlDoc.CreateElement("answer");
                    answer3.InnerXml = Server.UrlEncode(wrongAnswer4.Text);
                    answer3.SetAttribute("AnsType", "text");  //צריך לבדוק איך משנים בין טקסט לתמונה בסוג מסיח
                    answer3.SetAttribute("feedback", "false");
                    answer3.SetAttribute("ansID", "5");
                    answers.AppendChild(answer3);
                }
                // עריכת מסיח
                XmlNode answer5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='5']");
                XmlNode answer5type = xmlDoc.SelectSingleNode("//game[@GameCode='" + gamCodeS + "']/questions/ question[@id ='" + questionID + "']/answers/ answer[@ansID='5']/@AnsType");

                if (answer5type.InnerXml == "text")
                {
                    answer5.InnerXml = wrongAnswer4.Text;
                }
                if (answer5type.InnerXml == "img")
                {
                    answer5.InnerXml = imgName[4];
                }
            }
            // שמירת שינויים
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            AnsCount1.Visible = true;
            AnsCount2.Visible = true;
            AnsCount3.Visible = true;
            AnsCount4.Visible = true;
            AnsCount5.Visible = true;
        }

        // איפוס תיבות הטקסט
        theQuestion.Text = "";
        rightAnswer.Text = "";
        wrongAnswer.Text = "";
        wrongAnswer2.Text = "";
        wrongAnswer3.Text = "";
        wrongAnswer4.Text = "";

        // איפוס תמונה
        imagA0.ImageUrl = "~/images/imeg.png";
        imagA1.ImageUrl = "~/images/imeg.png";
        imagA2.ImageUrl = "~/images/imeg.png";
        imagA3.ImageUrl = "~/images/imeg.png";
        imagA4.ImageUrl = "~/images/imeg.png";
        imagA5.ImageUrl = "~/images/imeg.png";

        //איפוס מס מסיחים בשאלה חדשה
        Session["myansNum"] = 2;

        // הסתרה של המסיחים 3 ומעלה
        if (ansNum >= 3)
        {
            answer3.Visible = false;
        }
        if (ansNum >= 4)
        {
            answer4.Visible = false;
        }

        if (ansNum >= 5)
        {
            answer5.Visible = false;
            addAnswer.Enabled = true;
        }
        
       
       

        // שמירה לעץ
        XmlDataSource1.Save();
        GridView2.DataBind();

        //איפוס משתנה לבדיקת מצב עריכת שאלה
        Session["editQ"] = "0";
    }

    protected void goTosetting_Click1(object sender, EventArgs e)
    {
        // מעבר לעמוד הגדרות
        Session["newGame"] = "1";
        Response.Redirect("settings.aspx");
    }

    protected void addAnswer_Click(object sender, EventArgs e)
    {
        // הוספה של מסיח נוסף
        int ansNum = Convert.ToInt16(Session["myansNum"]);
        ansNum++;

        Session["myansNum"] = ansNum;   
        addAnswerCall();
    }

    // פונקציה המקבלת את התמונה שהועלתה , האורך והרוחב שאנו רוצים לתמונה ומחזירה את התמונה המוקטנת
    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);

        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto, new System.Drawing.Rectangle(destX, destY, destWidth, destHeight), new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }


    protected void addAnswerCall()
    {
        // הוספה של מסיח
        int ansNum = Convert.ToInt16(Session["myansNum"]);

        if (ansNum >= 3)
        {
            answer3.Visible = true;
            answer3.Attributes.Add("name", "3");

        }
        if (ansNum >= 4)
        {
            answer4.Visible = true;
            answer4.Attributes.Add("name", "4");
            string AddAnsCSS = addAnswer.CssClass;
            addAnswer.CssClass = "addAns";

        }

        if (ansNum >= 5)
        {
            answer5.Visible = true;
            answer5.Attributes.Add("name", "5");
            addAnswer.Enabled = false;
            addAnswer.CssClass = "addAnsNotActive";
        }


    }

    protected void goBack_Click(object sender, EventArgs e)
    {
        // מעבר לעמוד הראשי
        Session["newGame"] = "1";

        Response.Redirect("Default.aspx");
    }

    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // תחילה אנו מבררים מהו הקוד של המשחק בעץ
        ImageButton myEdit = (ImageButton)e.CommandSource;

        // אנו מושכים את איידי של השאלה באמצעות 
        string thequestion = myEdit.Attributes["theItemId"];
        Session["questionID"] = thequestion;

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור             
        switch (e.CommandName)
        {
            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה                    
            case "deletRowQ":
                deleteQuestion();
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לעריכת שאלה                     
            case "editRowQ":
                editQuestion();
                break;
        }
    }

    // הצגת פופ אפ לוידוא מחיקה
    protected void deleteQuestion()
    {
        // הצגת פופ אפ של מחיקה
        popUpCheck.Visible = true;
        backWindow.Visible = true;

        string questionToDelete = Session["questionID"].ToString();
        string gameCode = Session["gameCodeSession"].ToString();

        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode node = Document.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionToDelete + "']/questionText");

        string GameNameDelete = "האם אתה בטוח שברצונך למחוק את השאלה: " + Server.UrlDecode(node.InnerXml) + "?";
        checkDelite.Text = GameNameDelete;
    }
    //  לא רוצים למחוק, חוזרים למצב קודם
    protected void noDelite_Click(object sender, EventArgs e)
    {
        popUpCheck.Visible = false;
        backWindow.Visible = false;
    }

    // מחיקת שאלה
    protected void yesDelite_Click(object sender, EventArgs e)
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי הקוד שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        string questionToDelete = Session["questionID"].ToString();
        string gameCode = Session["gameCodeSession"].ToString();

        XmlDocument Document = XmlDataSource1.GetXmlDocument();

        XmlNode node = Document.SelectSingleNode("/pullTheCarrot/game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionToDelete + "']");
        node.ParentNode.RemoveChild(node);

        XmlDataSource1.Save();
        GridView2.DataBind();

        //ספירת מספר שאלות
        XmlNodeList MYquestionNum = Document.SelectNodes("//game[@GameCode='" + gameCode + "']/questions/question");

        // הצגת מספר שאלות
        questionCount.Text = Server.UrlDecode((MYquestionNum.Count).ToString());

        Session["myquestionNum"] = MYquestionNum.Count;

        theQuestion.Text = "";
        rightAnswer.Text = "";
        wrongAnswer.Text = "";
        wrongAnswer2.Text = "";
        wrongAnswer3.Text = "";
        wrongAnswer4.Text = "";

        imagA1.ImageUrl = "~/images/imeg.png";
        imagA2.ImageUrl = "~/images/imeg.png";
        imagA3.ImageUrl = "~/images/imeg.png";
        imagA4.ImageUrl = "~/images/imeg.png";
        imagA0.ImageUrl = "~/images/imeg.png";

        answer3.Visible = false;
        answer4.Visible = false;
        answer5.Visible = false;

        Session["myansNum"] = "2";
    }

    protected void editQuestion()
    {            
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));
        cancelEdit.Visible = true;
        string questionID = Session["questionID"].ToString();
        string gameCode = Session["gameCodeSession"].ToString();
        // איפוס תיבות הטקסט
        theQuestion.Text = "";
        rightAnswer.Text = "";
        wrongAnswer.Text = "";
        wrongAnswer2.Text = "";
        wrongAnswer3.Text = "";
        wrongAnswer4.Text = "";

        int ansNum = Convert.ToInt16(Session["myansNum"]);

        // הסתרה של המסיחים 3 ומעלה
        if (ansNum >= 3)
        {
            answer3.Visible = false;
        }
        if (ansNum >= 4)
        {
            answer4.Visible = false;
        }

        if (ansNum >= 5)
        {
            answer5.Visible = false;
            addAnswer.Enabled = true;
        }
        Session["editQ"] = "1";

        Page.Response.Write("<script>console.log('" + Session["editQ"] + "');</script>");
        // הצגת השאלה 
        XmlNode theQues = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/questionText");
        theQuestion.Text = Server.UrlDecode(theQues.InnerXml);

        string picQ = Session["picQ"].ToString();
        XmlNode Qimg = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/img");
        if (picQ == "img")
        {
            imagA0.ImageUrl = imagesLibPath + Server.UrlDecode(Qimg.InnerXml);
        }

        //הצגת המסיח הנכון
        XmlNode answer1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']");
        XmlNode ansTipe1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']/@AnsType");
        Page.Response.Write("<script>console.log('" + ansTipe1 + "');</script>");

        if (ansTipe1.InnerXml == "text")
        {
            rightAnswer.Text = Server.UrlDecode(answer1.InnerXml);

            imagA1.ImageUrl = "~/images/imeg.png";

            AnsCount1.Visible = true;
        }
        if (ansTipe1.InnerXml == "img")
        {
            imagA1.ImageUrl = imagesLibPath + Server.UrlDecode(answer1.InnerXml);
            rightAnswer.Text = "";
            AnsCount1.Visible = false;
        }

        //הצגת המסיחים הלא נכונים

        XmlNode answer2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']");
        XmlNode ansTipe2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']/@AnsType");

        if (ansTipe2.InnerXml == "text")
        {
            wrongAnswer.Text = Server.UrlDecode(answer2.InnerXml);
            imagA2.ImageUrl = "~/images/imeg.png";
            AnsCount2.Visible = true;
        }
        if (ansTipe2.InnerXml == "img")
        {
            imagA2.ImageUrl = imagesLibPath + Server.UrlDecode(answer2.InnerXml);
            wrongAnswer.Text = "";
            AnsCount2.Visible = false;
        }

        // בדיקת מספר מסיחים
        XmlNodeList numAnswer = xmlDoc.SelectNodes("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer");
        Page.Response.Write("<script>console.log('" + numAnswer.Count + "');</script>");
        Session["myansNum"] = numAnswer.Count;
        addAnswerCall();

        if (numAnswer.Count >= 3)
        {
            XmlNode answer3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']");
            XmlNode ansTipe3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']/@AnsType");


            if (ansTipe3.InnerXml == "text")
            {
                wrongAnswer2.Text = Server.UrlDecode(answer3.InnerXml);
                imagA3.ImageUrl = "~/images/imeg.png";
                AnsCount3.Visible = true;
            }
            if (ansTipe3.InnerXml == "img")
            {
                imagA3.ImageUrl = imagesLibPath + Server.UrlDecode(answer3.InnerXml);
                wrongAnswer2.Text = "";
                AnsCount3.Visible = false;
            }
        }
        if (numAnswer.Count >= 4)
        {
            XmlNode answer4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']");
            XmlNode ansTipe4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']/@AnsType");

            if (ansTipe4.InnerXml == "text")
            {
                wrongAnswer3.Text = Server.UrlDecode(answer4.InnerXml);
                imagA4.ImageUrl = "~/images/imeg.png";
                AnsCount4.Visible = true;
            }
            if (ansTipe4.InnerXml == "img")
            {
                imagA4.ImageUrl = imagesLibPath + Server.UrlDecode(answer4.InnerXml);
                wrongAnswer3.Text = "";
                AnsCount4.Visible = false;
            }
        }
        if (numAnswer.Count >= 5)
        {
            XmlNode answer5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
            XmlNode ansTipe5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']/@AnsType");

            if (ansTipe5.InnerXml == "text")
            {
                wrongAnswer4.Text = Server.UrlDecode(answer5.InnerXml);
                imagA5.ImageUrl = "~/images/imeg.png";
                AnsCount5.Visible = true;
            }
            if (ansTipe5.InnerXml == "img")
            {
                imagA5.ImageUrl = imagesLibPath + Server.UrlDecode(answer5.InnerXml);
                wrongAnswer4.Text = "";
                AnsCount5.Visible = false;
            }
        }
    }

    // מחיקת מסיח שלישי
    protected void deletAns1_Click(object sender, ImageClickEventArgs e)
    {
        // שליפת מצב עריכה או יצירת שאלה חדשה
        string checkEditQ = Session["editQ"].ToString();
        // מספר מסיחים קיים
        int ansNum = Convert.ToInt16(Session["myansNum"]);
        // במידה ויש שלוש מסיחים
        if (ansNum == 3)
        {
            addAnswer.CssClass = "addAns";
            // אם נמצאים בשאלה חדשה (לפני השמירה)
            if (checkEditQ == "0")
            {
                // המסיח עובר למצב מוסתר
                answer3.Visible = false;

            }
            // אם נמצאים במצב של עריכת שאלה
            else
            {
                answer3.Visible = false; // המסיח עובר למצב מוסתר

                XmlDocument xmlDoc = new XmlDocument(); // טעינת העץ 
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                string questionID = Session["questionID"].ToString(); // איידי של השאלה
                string gameCode = Session["gameCodeSession"].ToString(); // קוד של המשחק

                // המסיח שרוצים למחוק
                XmlNode anserDelet3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']");
                // מחיקת הענף מהעץ
                anserDelet3.ParentNode.RemoveChild(anserDelet3);
                // שמירה של העץ
                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

            }
            // מספר מסיחים שקיים בשאלה
            ansNum--;
            addAnswer.CssClass = "addAns";
            Session["myansNum"] = ansNum;
        }

        // אם קיימים 4 מסיחים
        if (ansNum == 4)
        {
            addAnswer.CssClass = "addAns";
            // ונמצאים במצב יצירת שאלה 
            if (checkEditQ == "0")
            {
                // שליפה של הנתונים ממסיח 4
                string myAns4 = wrongAnswer3.Text;
                // הזרקת הנתונים למסיח 3
                wrongAnswer2.Text = myAns4;
                //  מסתירים את מסיח 4
                answer4.Visible = false;
            }
            // במידה ונמצאים בעריכת שאלה
            else
            {
                // שליפה של הנתונים ממסיח 4
                string myAns4 = wrongAnswer3.Text;
                // הזרקת הנתונים למסיח 3
                wrongAnswer2.Text = myAns4;
                //  מסתירים את מסיח 4
                answer4.Visible = false;

                // טעינת עץ
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                // איידיי של השאלה
                string questionID = Session["questionID"].ToString();
                // קוד של השאלה
                string gameCode = Session["gameCodeSession"].ToString();

                // שליפה של מסיח 4 
                XmlNode anserDelet4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']");
                // מחיקת מסיח 4
                anserDelet4.ParentNode.RemoveChild(anserDelet4);
                // שמירה של העץ
                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));
            }
            // הורדת מספר המסיחים בשאלה
            ansNum--;


            Session["myansNum"] = ansNum;
        }

        if (ansNum == 5)
        {
            if (checkEditQ == "0")
            {
                string myAns4 = wrongAnswer3.Text;
                wrongAnswer2.Text = myAns4;

                string myAns5 = wrongAnswer4.Text;
                wrongAnswer3.Text = myAns5;

                answer5.Visible = false;

            }
            else
            {
                string myAns4 = wrongAnswer3.Text;
                wrongAnswer2.Text = myAns4;

                string myAns5 = wrongAnswer4.Text;
                wrongAnswer3.Text = myAns5;

                answer5.Visible = false;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                string questionID = Session["questionID"].ToString();
                string gameCode = Session["gameCodeSession"].ToString();

                XmlNode anserDelet5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
                anserDelet5.ParentNode.RemoveChild(anserDelet5);

                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));
            }


            ansNum--;
            Session["myansNum"] = ansNum;
            addAnswer.CssClass = "addAns";
            addAnswer.Enabled = true;
        }

    }

    protected void deletAns2_Click(object sender, ImageClickEventArgs e)
    {
        string checkEditQ = Session["editQ"].ToString();
        int ansNum = Convert.ToInt16(Session["myansNum"]);

        if (ansNum == 4)
        {
            if (checkEditQ == "0")
            {
                answer4.Visible = false;
            }
            else
            {
                answer4.Visible = false;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                string questionID = Session["questionID"].ToString();
                string gameCode = Session["gameCodeSession"].ToString();

                XmlNode anserDelet4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']");
                anserDelet4.ParentNode.RemoveChild(anserDelet4);

                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));
            }

            ansNum--;
            Session["myansNum"] = ansNum;
            addAnswer.CssClass = "addAns";
        }

        if (ansNum == 5)
        {
            if (checkEditQ == "0")
            {
                string myAns5 = wrongAnswer4.Text;
                wrongAnswer3.Text = myAns5;

                answer5.Visible = false;

            }
            else
            {
                string myAns5 = wrongAnswer4.Text;
                wrongAnswer3.Text = myAns5;

                answer5.Visible = false;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                string questionID = Session["questionID"].ToString();
                string gameCode = Session["gameCodeSession"].ToString();

                XmlNode anserDelet5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
                anserDelet5.ParentNode.RemoveChild(anserDelet5);

                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));
                addAnswer.Enabled = true;

            }

            ansNum--;
            Session["myansNum"] = ansNum;
            //addAnswer.CssClass = "addAns";
        }
    }

    protected void deletAns3_Click(object sender, ImageClickEventArgs e)
    {
        string checkEditQ = Session["editQ"].ToString();
        int ansNum = Convert.ToInt16(Session["myansNum"]);

        if (ansNum == 5)
        {
            if (checkEditQ == "0")
            {
                answer5.Visible = false;
            }
            else
            {
                answer5.Visible = false;

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

                string questionID = Session["questionID"].ToString();
                string gameCode = Session["gameCodeSession"].ToString();

                XmlNode anserDelet4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
                anserDelet4.ParentNode.RemoveChild(anserDelet4);

                xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));
            }

            ansNum--;
            Session["myansNum"] = ansNum;
            addAnswer.Enabled = true;
            addAnswer.CssClass = "addAns";
        }
    }

    protected void DeleteImg1_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        string checkEditQ = Session["editQ"].ToString();

        if (checkEditQ == "0")
        {
            imagA1.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode typeChange1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']/@AnsType");
            typeChange1.InnerXml = "text";
            XmlNode IMG1Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']");
            IMG1Delete.InnerXml = "";
            imagA1.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }


    }


    protected void ZoomImg1_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
        imagA1.Attributes.Add("style", "width: 250px;height: 200px; z-index: 12; position:relative; right:50px; top:-150px");
    }

    protected void Imageclose_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = false;
        imagA1.Attributes.Add("style", "");


    }

   
        protected void cancelEdit_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        string questionID = Session["questionID"].ToString();
        string gameCode = Session["gameCodeSession"].ToString();
        Session["editQ"] = "1";

        Page.Response.Write("<script>console.log('" + Session["editQ"] + "');</script>");
        // הצגת השאלה 
        XmlNode theQues = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/questionText");
        theQuestion.Text = Server.UrlDecode(theQues.InnerXml);

        //הצגת המסיח הנכון
        XmlNode answer1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']");
        XmlNode ansTipe1 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='1']/@AnsType");
        Page.Response.Write("<script>console.log('" + ansTipe1 + "');</script>");

        if (ansTipe1.InnerXml == "text")
        {
            rightAnswer.Text = Server.UrlDecode(answer1.InnerXml);

            imagA1.ImageUrl = "~/images/imeg.png";

            AnsCount1.Visible = true;
        }
        if (ansTipe1.InnerXml == "img")

        {
            imagA1.ImageUrl = imagesLibPath + Server.UrlDecode(answer1.InnerXml);
            rightAnswer.Text = "";
            AnsCount1.Visible = false;
        }

        //הצגת המסיחים הלא נכונים

        XmlNode answer2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']");
        XmlNode ansTipe2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']/@AnsType");

        if (ansTipe2.InnerXml == "text")
        {
            wrongAnswer.Text = Server.UrlDecode(answer2.InnerXml);
            imagA2.ImageUrl = "~/images/imeg.png";
            AnsCount2.Visible = true;
        }
        if (ansTipe2.InnerXml == "img")
        {
            imagA2.ImageUrl = imagesLibPath + Server.UrlDecode(answer2.InnerXml);
            wrongAnswer.Text = "";
            AnsCount2.Visible = false;
        }

        // בדיקת מספר מסיחים
        XmlNodeList numAnswer = xmlDoc.SelectNodes("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer");
        Page.Response.Write("<script>console.log('" + numAnswer.Count + "');</script>");
        Session["myansNum"] = numAnswer.Count;
        addAnswerCall();

        if (numAnswer.Count >= 3)
        {
            XmlNode answer3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']");
            XmlNode ansTipe3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']/@AnsType");


            if (ansTipe3.InnerXml == "text")
            {
                wrongAnswer2.Text = Server.UrlDecode(answer3.InnerXml);
                imagA3.ImageUrl = "~/images/imeg.png";
                AnsCount3.Visible = true;
            }
            if (ansTipe3.InnerXml == "img")
            {
                imagA3.ImageUrl = imagesLibPath + Server.UrlDecode(answer3.InnerXml);
                wrongAnswer2.Text = "";
                AnsCount3.Visible = false;
            }
        }
        if (numAnswer.Count >= 4)
        {
            XmlNode answer4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']");
            XmlNode ansTipe4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']/@AnsType");

            if (ansTipe4.InnerXml == "text")
            {
                wrongAnswer3.Text = Server.UrlDecode(answer4.InnerXml);
                imagA4.ImageUrl = "~/images/imeg.png";
                AnsCount4.Visible = true;
            }
            if (ansTipe4.InnerXml == "img")
            {
                imagA4.ImageUrl = imagesLibPath + Server.UrlDecode(answer4.InnerXml);
                wrongAnswer3.Text = "";
                AnsCount4.Visible = false;
            }
        }
        if (numAnswer.Count >= 5)
        {
            XmlNode answer5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
            XmlNode ansTipe5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']/@AnsType");

            if (ansTipe5.InnerXml == "text")
            {
                wrongAnswer4.Text = Server.UrlDecode(answer5.InnerXml);
                imagA5.ImageUrl = "~/images/imeg.png";
                AnsCount5.Visible = true;
            }
            if (ansTipe5.InnerXml == "img")
            {
                imagA5.ImageUrl = imagesLibPath + Server.UrlDecode(answer5.InnerXml);
                wrongAnswer4.Text = "";
                AnsCount5.Visible = false;
            }
        }

    }


    protected void DeleteImgQ_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        string checkEditQ = Session["editQ"].ToString();

        if (checkEditQ == "0")
        {
            imagA0.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode IMG0Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/img");
            IMG0Delete.InnerXml = "";
            imagA0.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }


    }

    protected void ZoomImgQ_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
    }

    protected void DeleteImgA2_Click(object sender, ImageClickEventArgs e)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));


        string checkEditQ = Session["editQ"].ToString();

        if (checkEditQ == "0")
        {
            imagA2.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode typeChange2 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']/@AnsType");
            typeChange2.InnerXml = "text";
            XmlNode IMG1Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='2']");
            IMG1Delete.InnerXml = "";
            imagA2.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }
    }

    protected void ZoomImgA2_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
    }



    protected void DeleteImgA3_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        string checkEditQ = Session["editQ"].ToString();
        if (checkEditQ == "0")
        {
            imagA3.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode typeChange3 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']/@AnsType");
            typeChange3.InnerXml = "text";
            XmlNode IMG3Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='3']");
            IMG3Delete.InnerXml = "";
            imagA3.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }
    }

    protected void ZoomImgA3_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
    }


    protected void DeleteImgA4_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        string checkEditQ = Session["editQ"].ToString();
        if (checkEditQ == "0")
        {
            imagA4.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode typeChange4 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']/@AnsType");
            typeChange4.InnerXml = "text";
            XmlNode IMG4Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='4']");
            IMG4Delete.InnerXml = "";
            imagA4.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }
    }
    protected void ZoomImgA4_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
    }

    protected void DeleteImgA5_Click(object sender, ImageClickEventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("trees/XMLFile.xml"));

        string checkEditQ = Session["editQ"].ToString();
        if (checkEditQ == "0")
        {
            imagA5.ImageUrl = "/images/imeg.png";
        }
        else
        {
            string gameCode = Session["GameCodeSession"].ToString();
            string questionID = Session["questionID"].ToString();
            XmlNode typeChange5 = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']/@AnsType");
            typeChange5.InnerXml = "text";
            XmlNode IMG5Delete = xmlDoc.SelectSingleNode("//game[@GameCode='" + gameCode + "']/questions/question[@id='" + questionID + "']/answers/answer[@ansID='5']");
            IMG5Delete.InnerXml = "";
            imagA5.ImageUrl = "/images/imeg.png";
            xmlDoc.Save(Server.MapPath("trees/XMLFile.xml"));

        }
    }
    protected void ZoomImgA5_Click(object sender, ImageClickEventArgs e)
    {
        BigPic.Visible = true;
    }
}

    //protected void tempSave_Click()
    //{
    //    Page.Response.Write("<script>console.log('hiiiiii');</script>");

    //    int ansNum = Convert.ToInt16(Session["myansNum"]);
    //    string[] myarray = new string[ansNum];////מערך לקליטת סוג המסיח
    //    string[] myImgName = new string[ansNum];

    //    //לולאה שעוברת על כל התמונות שהועלו ומוסיפה אותם לתיקיית הפרויקט
    //    //ניתן לעשות גם קוד כפול נפרד עם השמות של הפקדים ללא צורך בשימוש ב FindControl
    //    for (int i = 1; i <= ansNum; i++)
    //    {
    //        string fileType = ((FileUpload)FindControl("FileUpload" + i)).PostedFile.ContentType;
    //        string imageNewName;

    //        if (fileType.Contains("image")) //בדיקה האם הקובץ שהוכנס הוא תמונה
    //        {
    //            string fileName = ((FileUpload)FindControl("FileUpload" + i)).PostedFile.FileName;// הנתיב המלא של הקובץ עם שמו האמיתי של הקובץ
    //            string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));  // הסיומת של הקובץ
    //            string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss_ffff");    //לקיחת הזמן האמיתי למניעת כפילות בתמונות
    //            imageNewName = myTime + i + endOfFileName;// חיבור השם החדש עם הסיומת של הקובץ
    //            ((FileUpload)FindControl("FileUpload" + i)).PostedFile.SaveAs(Server.MapPath(tempImgSave) + imageNewName);//שמירה של הקובץ לספרייה בשם החדש שלו
    //            ((ImageButton)FindControl("imagA" + i)).ImageUrl = tempImgSave + imageNewName;//הצגה של הקובץ החדש מהספרייה

    //        }
    //        else
    //        {
    //            // הקובץ אינו תמונה ולכן לא ניתן להעלות אותו
    //        }
    //    }

    //}


