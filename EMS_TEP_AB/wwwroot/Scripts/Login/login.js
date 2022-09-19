
var baseUrl = 'https://localhost:44336'
$(document).ready(function () {
    $('#loginSubmit').click(function () {
        
        var data = {};
        data.user_name = $('#userName').val();
        data.password = $('#password-field').val();

        try {
            $.ajax({
                url: baseUrl + "/Security/Login",
                type: 'POST',
                async: true,
                data: data,
                success: function (result) {
                    
                    if (result.is_Authenticated == true) {
                        var getAttendence = {};
                        getAttendence.userId = result.userId;
                        getAttendence.sessionToken = result.session_token;
                        $.ajax({
                            url: baseUrl + "/Attendance/GetAttendenceList",
                            type: 'POST',
                            async: true,
                            data: getAttendence,
                            success: function (result) {                                
                                console.log(result);
                            },
                            error: function (result) {

                            }
                        });
                    }                    
                },
                error: function (result) {
                   
                }
            });
        }
        catch (err) {
            alert(err)
        }
    })
})