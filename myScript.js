///////////עמוד JS הגדרות


//כאשר העמוד נטען


$(document).ready(function () {


    var newgamename = document.getElementById("addGameTB").value;
    var myfeedback = document.getElementById("endFeedback").value;

    checkActive(newgamename, myfeedback);


    if ((newgamename.length < 2) && (myfeedback.length < 2)) {
        // כפתור לא פעיל להגדרות
        document.getElementById("creatGame").disabled = true;
        document.getElementById("creatGame").classList.remove("saveActive");
        document.getElementById("creatGame").classList.add("NoActive");
   

      ////הצגת הטולטיפ
        document.getElementById("tooltip1").classList.add("tooltipQtxt");
        document.getElementById("tooltip1").classList.remove("tooltipQtxtNOACTIVE");
       

    }


    if ((newgamename.length >= 2) && (feedback.length >= 2)) {

        document.getElementById("creatGame").classList.remove("NoActive");
        document.getElementById("creatGame").classList.add("saveActive");
        document.getElementById("creatGame").disabled = false;
        // תצוגה ספירת התווים
        document.getElementById("nameCount").textContent = newgamename.length + "/30";
        ///הסתרת הטולטיפ
        document.getElementById("tooltip1").classList.add("tooltipQtxtNOACTIVE");
        document.getElementById("tooltip1").classList.remove("tooltipQtxt");

    }

    var oldFeedback = document.getElementById("endFeedback").value;
    //  ספירת התווים תצוגה 
    document.getElementById("feedbackCount").textContent = oldFeedback.length + "/60";

    if (oldFeedback.length > 2) {

        document.getElementById("feedbackCount").classList.remove("redT");
        document.getElementById("feedbackCount").classList.add("greenT");
    }


    //בהקלדה בתיבת הטקסט
    $(".CharacterCount").keyup(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

    //בהעתקה של תוכן לתיבת הטקסט
    $(".CharacterCount").on("paste", function () {
        checkCharacter($(this));//קריאה לפונקציה שבודקת את מספר התווים
    });


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

        colorChenge();

        var myfeedback = document.getElementById("endFeedback").value;

        var newgamename = document.getElementById("addGameTB").value;
        checkActive(newgamename, myfeedback);

    }

});

// פונקציה שצובעת את המספרים ומפעילה את הכפתור
function colorChenge() {

    var GameName = document.getElementById("addGameTB").value;
    var myfeedback = document.getElementById("endFeedback").value;

    if (GameName.length >= 2) {
        document.getElementById("nameCount").classList.remove("redT");
        document.getElementById("nameCount").classList.add("greenT");

    }
    else {
        document.getElementById("nameCount").classList.remove("greenT");
        document.getElementById("nameCount").classList.add("redT");
      
    }


    var endFeedback = document.getElementById("endFeedback").value;

    if (endFeedback.length >= 2) {
        document.getElementById("feedbackCount").classList.add("greenT");
        document.getElementById("feedbackCount").classList.remove("redT");
    }
    else {
        document.getElementById("feedbackCount").classList.remove("greenT");
        document.getElementById("feedbackCount").classList.add("redT");
    }


    // כפתור פעיל לאחר כתיבת שם המשחק
    document.getElementById("creatGame").disabled = false;
    document.getElementById("tooltip1").classList.add("tooltipQtxtNOACTIVE");
    document.getElementById("tooltip1").classList.remove("tooltipQtxt");


}

function checkActive(newgamename, feedback) {

    if ((newgamename.length) < 2 && (feedback.length < 2) ) {
        // כפתור לא פעילו להגדרות
        document.getElementById("creatGame").disabled = true;
        document.getElementById("creatGame").classList.remove("saveActive");
        document.getElementById("creatGame").classList.add("NoActive");
        document.getElementById("tooltip1").classList.add("tooltipQtxt");
        document.getElementById("tooltip1").classList.remove("tooltipQtxtNOACTIVE");
    }

    if ((newgamename.length >= 2) && (feedback.length >=2)) {
       
        document.getElementById("creatGame").classList.remove("NoActive");
        document.getElementById("creatGame").classList.add("saveActive");
        document.getElementById("creatGame").disabled = false;
        document.getElementById("tooltip1").classList.add("tooltipQtxtNOACTIVE");
        document.getElementById("tooltip1").classList.remove("tooltipQtxt");

    }


}


