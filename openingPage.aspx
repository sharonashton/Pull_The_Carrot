<%@ Page Language="C#" AutoEventWireup="true" CodeFile="openingPage.aspx.cs" Inherits="openingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <meta charset="utf-8"/>
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>משוך בגזר</title>
    <meta name="description" content="name of game" />
    <meta name="keywords" content="" />
    <meta name="author" content="names" />


    <!-- CSS -->
    <link href="Style/menue.css" rel="stylesheet" />
    <script src="jScripts/opening.js"></script>
    <!-- Scripts -->
     <script src="jScripts/jquery-1.12.0.min2.js"></script>
   <%-- <script src="jScripts/myScript1.js"></script>--%>

    <!--jQuery library-->
    <script src="jScripts/jquery-1.12.0.min.js"></script>
    <!--הקוד שמפעיל את תפריט הניווט-->
    <script src="jScripts/menu.js"></script>

   


</head>
<body class="openpageB">
    <div id="container">
        <header>
                   <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img src="images/Untitled-1.png" /> <!--הלוגו של המשחק שלכם-->
                <!--<p>משוך בגזר</p>-->
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                     <li><a class="helpFalse">עזרה</a></li>
                    <li><a class="about">אודות</a></li>
                    <li><a href="index.html" >למשחק</a></li>
                   
                </ul>
            </nav>
        </header>
         <div id="aboutDiv" class="popUp bounceInDown hide">
            
                <a class="closeAbout">X</a>
                <p class="headP">אודות</p>
   
            <img class="imgtop" src="images/Untitled-1.png" />
           
                <p id="madeby"><b>אפיון ופיתוח</b>:</p>        
            
            <p class="names" id="noam">
                נועם אהרון

            </p>

            <p class="names" id="sharon">
                שרון אשתון

            </p>

            <p class="names" id="limor">
                לימור גולד

            </p>
            <p>
                <img class="Pics" id="noamPic" src="images/noam.png" />
                <img class="Pics" id="sharonPic" src="images/sharon.png" />
                <img class="Pics" id="limorPic" src="images/limor.png" />

            </p>
               
                        <p >
                אופיין ופותח במסגרת פרויקט בקורסים:
             </p> <p>
                 סביבות לימוד אינטראקטיביות 2 (13122 ) +תכנות אינטראקציה ואנימציה 2 (13131 ) +תכנות 2 (13211)
 </p><p>תשפ"א</p>
            <a class="HIT" href="http://www.hit.ac.il/">  <img class="HIT" src="images/HIT.png" /> </a>
            <p class="Telem"> <a class="Telem" href="http://www.hit.ac.il/telem/overview">  הפקולטה לטכנולוגיית למידה</a></p>
        </div>


        
    <form id="form1" runat="server">
       
        <div id="gameCodeStart">
             <asp:Label ID="message" class="redT" runat="server" Text="Label">שם המשתמש או הסיסמה לא נכונים </asp:Label>

            <p> שם משתמש:</p>
            <asp:TextBox ID="admin" runat="server"></asp:TextBox>
            <p> סיסמה:</p>
            <asp:TextBox ID="password"  TextMode="Password" runat="server"></asp:TextBox>
            
            <br />
            <br />
            <asp:Button ID="enter" runat="server" Text="כניסה" OnClick="enter_Click" />

        </div>
        
    </form>
        </div>
</body>
</html>
