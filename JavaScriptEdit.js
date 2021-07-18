
window.addEventListener('DOMContentLoaded', (event) => {

    var trueans = document.getElementById("rightAnswer").value;
    if (trueans.length >= 1) {
        document.getElementById("imagA1").src = "/images/imeg2.png";
        document.getElementById("imagA1").disabled = true;
    }
    if (trueans.length == 0) {
        document.getElementById("imagA1").disabled = false;
    }
    //document.getElementById("change1").hidden = true;
  
    //    ///////////////////////////תנאי לפרסום השאלה
    document.getElementById("saveQ").disabled = true;
    var QuestionText = document.getElementById("theQuestion").value;//שמירת הערך מהשאלה
    var Ans1Text = document.getElementById("rightAnswer").value;//שמירת הערך מתיבת טקסט במסיח ראשון
    var Ans2Text = document.getElementById("wrongAnswer").value;//שמירת הערך מתיבת טקסט במסיח שני
  
    var Ans1IMG = document.getElementById("imagA1").value;//שמירת התמונה במסיח ראשון
    var Ans2IMG = document.getElementById("imagA2").value;//שמירת התמונה במסיח שני

    if ((QuestionText.length >= 2) && (Ans1Text.length >= 2) && (Ans2Text.length >= 2)) {
        document.getElementById("saveQ").disabled = false;
        document.getElementById("saveQ").classList.remove("EditNOActive");
        document.getElementById("saveQ").classList.add("saveQAvailble");

    }
    else {
        document.getElementById("saveQ").disabled = true;
    }
    /////////////////////////////////////////

});

$(document).ready(function () {

    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });

    var pic1check = document.getElementsByClassName("pic1");
       
    if (pic1check == "pic1") {
        alert("YESSSS");
    }


   
    ///////////////////////////////////////////////////////////////////////////////////
    // הצגת מצב התווים הנוכחי בשאלה
    ///אם מספר התווים בשאלה גדול מ0 תשנה לו את הספירה לפי כמות התווים
    var TheQuestionnn = document.getElementById("theQuestion").value;
    if (TheQuestionnn.length > 0) {
    document.getElementById("QuestCount").textContent = TheQuestionnn.length + "/45";
    }
    ///אם גדול שווה ל2 תצבע בירוק
    if (TheQuestionnn.length >= 2) {
        document.getElementById("QuestCount").style.color = "green";
      ///אם קטן מ2 תצבע באדום
    }
    if (TheQuestionnn.length < 2) {
        document.getElementById("QuestCount").style.color = "red";
     
    }

    ////הצגת מצב התווים במסיח ראשון
    ///אם מספר התווים במסיח הראשון גדול מ0 תשנה לו את הספירה לפי כמות התווים
    var trueans = document.getElementById("rightAnswer").value;
    if (trueans.length > 0) {
    document.getElementById("AnsCount1").textContent = trueans.length + "/20";
    }
     ///אם גדול שווה ל2 תצבע בירוק
    if (trueans.length >= 2) {
        document.getElementById("AnsCount1").style.color = "green";
       ///אם קטן מ2 תצבע באדום
    }
    if (trueans.length < 2) {
        document.getElementById("AnsCount1").style.color = "red";
      
    }
     ///וכן הלאה לשאר המסיחים
    ////הצגת מצב התווים במסיח השני
    var wrong1 = document.getElementById("wrongAnswer").value;

    if (wrong1.length > 0) { 
        document.getElementById("AnsCount2").textContent = wrong1.length + "/20";
    }
    if (wrong1.length >= 2) {
        document.getElementById("AnsCount2").style.color = "green";  
    }
    if (wrong1.length < 2) {
        document.getElementById("AnsCount2").style.color = "red";
    }
    ////הצגת מצב התווים במסיח השלישי
    var mycheck3 = document.getElementsByName("3");
    if (mycheck3.length == 1) {
        var wrong2 = document.getElementById("wrongAnswer2").value;
        if (wrong2.length > 0) {
            document.getElementById("AnsCount3").textContent = wrong2.length + "/20";
        }
        if (wrong2.length >= 2) {
            document.getElementById("AnsCount3").style.color = "green";  
        }
        if (wrong2.length < 2) {
            document.getElementById("AnsCount3").style.color = "red";
        }
    }
    ////הצגת מצב התווים במסיח הרביעי
    var mycheck4 = document.getElementsByName("4");
    if (mycheck4.length == 1) {
        var wrong3 = document.getElementById("wrongAnswer3").value;
        if (wrong3.length > 0) {
            document.getElementById("AnsCount4").textContent = wrong3.length + "/20";
        }
        if (wrong3.length >= 2) {
            document.getElementById("AnsCount4").style.color = "green";
        }
        if (wrong3.length < 2) {
            document.getElementById("AnsCount4").style.color = "red";
            }
    }
    ////הצגת מצב התווים במסיח החמישי
    var mycheck5 = document.getElementsByName("5");
    if (mycheck5.length == 1) {
        var wrong4 = document.getElementById("wrongAnswer4").value;
        if (wrong4.length > 0) {  
            document.getElementById("AnsCount5").textContent = wrong4.length + "/20";
        }
        if (wrong4.length >= 2) {
            document.getElementById("AnsCount5").style.color = "green";
        }
        if (wrong4.length < 2) {
            document.getElementById("AnsCount5").style.color = "red";
        }
    }
        //////////////////////////////////////////////////////////////////////////////

    //פונקציה שמקבלת את תיבת הטקסט שבה מקלידים ובודקת את מספר התווים
    function checkCharacter(myTextBox) {
        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.val().length;
        //משתנה המכיל את מספר התווים שמוגבל לתיבה זו
        var CharacterLimit = myTextBox.attr("CharacterLimit");
        //בדיקה האם ישנה חריגה במספר התווים
        if (countCurrentC > CharacterLimit) {
            //מחיקת התווים המיותרים בתיבה
            myTextBox.val(myTextBox.val().substring(0, CharacterLimit));
            //עדכון של מספר התווים הנוכחי
            countCurrentC = CharacterLimit;
        }
        //משתנה המקבל את שם הלייבל המקושר לאותה תיבת טקסט 
        var LableToShow = myTextBox.attr("CharacterForLabel");
        //הדפסה כמה תווים הוקלדו מתוך כמה
        $("#" + LableToShow).text(countCurrentC + "/" + CharacterLimit);

        //תנאי לבדיקת המסיח הראשון - אם נוסף טקסט בתיבה תחליף את התמונה ב
        var trueans = document.getElementById("rightAnswer").value;

        if (trueans.length >= 1) {
            document.getElementById("imagA1").src = "/images/imeg2.png";
            document.getElementById("imagA1").disabled = true;
          
        }
        if (trueans.length == 0) {
           //document.getElementById("imagA1").src = "/images/imeg.png";
           document.getElementById("imagA1").disabled = false;
          
        }

        var wrongAns1 = document.getElementById("wrongAnswer").value;

        if (wrongAns1.length >= 1) {
            document.getElementById("imagA2").src = "/images/imeg2.png";
            document.getElementById("imagA2").disabled = true;
        }
        if (wrongAns1.length == 0) {
            document.getElementById("imagA2").src = "/images/imeg.png";
            document.getElementById("imagA2").disabled = false;
        }

     

      
        var wrongAns2check = document.getElementsByName("3");
        
        if (wrongAns2check == "3") {
            var wrongAns2 = document.getElementById("wrongAnswer2").value;

            if (wrongAns2.length >= 1) {
                document.getElementById("imagA3").src = "/images/imeg2.png";
                document.getElementById("imagA3").disabled = true;
            }
            if (wrongAns2.length == 0) {
                document.getElementById("imagA3").src = "/images/imeg.png";
                document.getElementById("imagA3").disabled = false;
            }

        }
      


        var wrongAns3check = document.getElementsByName("4");

        if (wrongAns3check == "4") {
            var wrongAns3 = document.getElementById("wrongAnswer3").value;
            if (wrongAns3.length >= 1) {
                document.getElementById("imagA4").src = "/images/imeg2.png";
                document.getElementById("imagA4").disabled = true;
            }
            if (wrongAns3.length == 0) {
                document.getElementById("imagA4").src = "/images/imeg.png";
                document.getElementById("imagA4").disabled = false;
            }

        }


        var wrongAns4check = document.getElementsByName("5");

        if (wrongAns4check == "5") {

            var wrongAns4 = document.getElementById("wrongAnswer4").value;

            if (wrongAns4.length >= 1) {
                document.getElementById("imagA5").src = "/images/imeg2.png";
                document.getElementById("imagA5").disabled = true;
            }
            if (wrongAns4.length == 0) {
                document.getElementById("imagA5").src = "/images/imeg.png";
                document.getElementById("imagA5").disabled = false;
            }
        }
    


        //    ///////////////////////////תנאי לפרסום השאלה
        //document.getElementById("saveQ").disabled = true;
        //var QuestionText = document.getElementById("theQuestion").value;//שמירת הערך מהשאלה
        //var Ans1Text = document.getElementById("rightAnswer").value;//שמירת הערך מתיבת טקסט במסיח ראשון
        //var Ans2Text = document.getElementById("wrongAnswer").value;//שמירת הערך מתיבת טקסט במסיח שני

        //var Ans1IMG = document.getElementById("imagA1").value;//שמירת התמונה במסיח ראשון
        //var Ans2IMG = document.getElementById("imagA2").value;//שמירת התמונה במסיח שני

        //if ((QuestionText.length >= 2) && ((Ans1Text.length >= 2) || (Ans1IMG != "~/images/imeg.png")) && ((Ans2Text.length >= 2) || (Ans2IMG != "~/images/imeg.png"))) {
        //    document.getElementById("saveQ").disabled = false;
        //    document.getElementById("saveQ").classList.remove("EditNOActive");
        //    document.getElementById("saveQ").classList.add("saveQAvailble");

        //}
        //else {
        //    document.getElementById("saveQ").disabled = true;
        //}
    /////////////////////////////////////////

        colorChenge();

    }


    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר אחד
    $("#FileUpload1").change(function () {

      

        if (this.files && this.files[0]) {
            var reader = new FileReader();
            
            reader.onload = function (e) {
                $('#imagA1').attr('src', e.target.result);
               // $(tempSave).click();
             

                document.getElementById("rightAnswer").disabled = true;
                document.getElementById("imagA1").classList.add("pic1");
                
               
            }
            reader.readAsDataURL(this.files[0]);
           
        }
    });

    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר שתיים
    $("#FileUpload2").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imagA2').attr('src', e.target.result);
            //    $(tempSave).click();
              
                document.getElementById("wrongAnswer").disabled = true;
            }

            reader.readAsDataURL(this.files[0]);
        }
    });

    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר שלוש
    $("#FileUpload3").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imagA3').attr('src', e.target.result);
               // $(tempSave).click();
               
                document.getElementById("wrongAnswer2").disabled = true;
            }

            reader.readAsDataURL(this.files[0]);
        }
    });

    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר ארבע
    $("#FileUpload4").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imagA4').attr('src', e.target.result);
               
               /// $(tempSave).click();
                document.getElementById("wrongAnswer3").disabled = true;
            }

            reader.readAsDataURL(this.files[0]);
        }
    });


    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר חמש
    $("#FileUpload5").change(function () {
        if (this.files && this.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('#imagA5').attr('src', e.target.result);
              //  $(tempSave).click();
               
                document.getElementById("wrongAnswer4").disabled = true;
            }
            reader.readAsDataURL(this.files[0]);
        }
    });
    //לאחר שלחצנו על התמונה שרצינו לבחור - תמונה מספר חמש התמונה בשאלה
    $("#FileUpload0").change(function () {

      
        if (this.files && this.files[0]) {
            var reader = new FileReader();
            
            reader.onload = function (e) {
                $('#imagA0').attr('src', e.target.result);
                
            }
            reader.readAsDataURL(this.files[0]);
            
            
        }
    });

    //$("#addAnswer").click(function (e) {
      
    //    e.preventDefault();

    //});
});

// פונקציה שצובעת את המספרים ומפעילה את הכפתור
function colorChenge() {

    //צביעת מספר תווים בשאלה
    var QuestionText = document.getElementById("theQuestion").value;

    if (QuestionText.length >= 2) {
        document.getElementById("QuestCount").style.color = "green";
    }
    else {
        document.getElementById("QuestCount").style.color = "red";
    }

     //צביעת מספר תווים במסיח ראשון

    var Ans1Text = document.getElementById("rightAnswer").value;


    if (Ans1Text.length >= 2) {
        document.getElementById("AnsCount1").style.color = "green";
        }
    else {
        document.getElementById("AnsCount1").style.color = "red";
     }


    //צביעת מספר תווים במסיח שני
    var Ans2Text = document.getElementById("wrongAnswer").value;
    if (Ans2Text.length >= 2) {
        document.getElementById("AnsCount2").style.color = "green";
    }
    else {
        document.getElementById("AnsCount2").style.color = "red";
    }

    //צביעת מספר תווים במסיח שלישי
    var mycheck3 = document.getElementsByName("3");
    if (mycheck3.length == 1) {
        var Ans3Text = document.getElementById("wrongAnswer2").value;
        if (Ans3Text.length >= 2) {
            document.getElementById("AnsCount3").style.color = "green";
        }
        else {
            document.getElementById("AnsCount3").style.color = "red";
        }
    }
    //צביעת מספר תווים במסיח רביעי
    var mycheck4 = document.getElementsByName("4");
    if (mycheck4.length == 1) {
        var Ans4Text = document.getElementById("wrongAnswer3").value;
        if (Ans4Text.length >= 2) {
            document.getElementById("AnsCount4").style.color = "green";
        }
        else {
            document.getElementById("AnsCount4").style.color = "red";
        }
    }
    //צביעת מספר תווים במסיח חמישי
    var mycheck5 = document.getElementsByName("5");
    if (mycheck5.length == 1) {
        var Ans5Text = document.getElementById("wrongAnswer4").value;
        if (Ans5Text.length >= 2) {
            document.getElementById("AnsCount5").style.color = "green";
        }
        else {
            document.getElementById("AnsCount5").style.color = "red";
        }
    }
    /////////////////////////////תנאי לפרסום השאלה

    var QuestionText = document.getElementById("theQuestion").value;//שמירת הערך מהשאלה
    var Ans1Text = document.getElementById("rightAnswer").value;//שמירת הערך מתיבת טקסט במסיח ראשון
    var Ans2Text = document.getElementById("wrongAnswer").value;
    if ((QuestionText.length >= 2) && (Ans1Text.length >= 2) && (Ans2Text.length >= 2)) {
        document.getElementById("saveQ").disabled = false;
        document.getElementById("saveQ").classList.remove("EditNOActive");
        document.getElementById("saveQ").classList.add("saveQAvailble");

    }
    else {
        document.getElementById("saveQ").disabled = true;
    }
    ///////////////////////////////////////////

}

//פעולה שמתרחשת בלחיצה על התמונה בשאלה ופותחת את חלון בחירת התמונה לשאלה
function openFileUploader0() {
    //document.getElementById("PanelQuestionIMGEdit").classList.add("PanelQuestionIMGEditShow");
    $('#FileUpload0').click();
    
}

    //פעולה שמתרחשת בלחיצה על התמונה הראשונה ופותחת את חלון בחירת התמונה הראשונה
    function openFileUploader1() {
        $('#FileUpload1').click();
    }

    //פעולה שמתרחשת בלחיצה על התמונה השניה ופותחת את חלון בחירת התמונה השניה
    function openFileUploader2() {
        $('#FileUpload2').click();
    }

    //פעולה שמתרחשת בלחיצה על התמונה השניה ופותחת את חלון בחירת התמונה השלישית
    function openFileUploader3() {
        $('#FileUpload3').click();
    }

    //פעולה שמתרחשת בלחיצה על התמונה השניה ופותחת את חלון בחירת התמונה הרביעית
    function openFileUploader4() {
        $('#FileUpload4').click();
    }

    //פעולה שמתרחשת בלחיצה על התמונה השניה ופותחת את חלון בחירת התמונה החמישית
    function openFileUploader5() {
        $('#FileUpload5').click();
}




