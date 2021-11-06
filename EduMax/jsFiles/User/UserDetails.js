function ChangeUserStatus(id) {
    //alert("hello: " + id);

    //Ajax call using jquery to retrieve User status.
    $.ajax({
        url: "/User/ChangeStatus/" + id,
        method: "GET",
        async: false,
        dataType: 'json',
        //contentType: "application/json",

        //If data is returned successfully and request is successful then the following line executes.
        //If the data is successfully returned, it will be returned in json format and will be assigned in the data
        //variable which is inside the function parameter
        success: function (data) {
            var str = ''; //empty variable
            var userStatus = "";
            //console.log(data);
            //alert("success");

            //If the returned data, which is user status is "0" then following line executes
            if (data.Status == "0") {
                //Since returned data is "0" that means user status is "Inactive", therefore the "Inactive"
                //is assigned to userStatus variable
                userStatus = "Inactive";
                //alert("Changed Status to Inactive");

                str += userStatus + "<input type='button' class=\"btn btn-link\" onclick='ChangeUserStatus(" + id + ")' value='Change Status'>";
                $("#userStatus").html(str);
            }
            //Else the returned data, which is user status is "1" then following line executes
            else {
                //Since returned data is "1" that means user status is "Active", therefore the "Active"
                //is assigned to userStatus variable
                userStatus = "Active";
                //alert("Changed Status to Active");

                //Assigning the userStatus along with concatenation of button type input to str varaible
                str += userStatus + "<input type='button' class=\"btn btn-link\" onclick='ChangeUserStatus(" + id + ")' value='Change Status'>";
                //The str variable will be passed as a html to html id "#userStatus"
                $("#userStatus").html(str);
                //alert("done");
            }
        },

        //If the data returned gets error the following line executes.
        error: function (err) {
            alert("error");
            //console.log(err)
        }
    })
    //alert("done");
}