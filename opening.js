$(document).ready(function () {


    var userName = document.getElementById("admin").value;
    var userpass = document.getElementById("password").value;
    console.log("למהההההה")
    if (userName.length == 0 || userpass.length == 0) {
        document.getElementById("enter").disabled = true;
        console.log("dis")
    }
    if (userName.length >= 1 || userpass.length >= 1) {
        document.getElementById("enter").disabled = false;
        console.log("able")
    }

    checkCharacter($(".CharacterCount"));

    //בטעינה של העמוד בכל פעם שנכנסים לאובייקט כזה
    $(".CharacterCount").each(function () {
        checkCharacter($(this)); //קריאה לפונקציה שבודקת את מספר התווים
    });

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
        console.log("in")
        //משתנה למספר התווים הנוכחי בתיבת הטקסט
        var countCurrentC = myTextBox.value;

        if (countCurrentC.length == 0) {
            document.getElementById("enter").disabled = true;
            console.log("in1");
        }

        else {
            document.getElementById("enter").disabled = false;
            console.log("Out1");
        }


    }
});