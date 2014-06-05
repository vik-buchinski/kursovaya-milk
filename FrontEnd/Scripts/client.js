var requestsLaunched = 0;
var server = {
    getLoginView: function (callback) {
        server.doRequest("/Account/SignIn", "GET", "", callback);
    },

    goToFileUpload: function (memberId, categoryId, callback) {
        server.doRequest("/Conference/UploadWork", "GET", { member_id: memberId, category_id: categoryId }, callback);
    },

    goToMemberRegistration: function (categoryId, callback) {
        var params = { conference_category: categoryId };
        server.doRequest("/Conference/RegistrateMember", "GET", params, callback);
    },

    openConferenceRegistration: function (callback) {
        server.doRequest("/Conference/OpenRegistration", "POST", "", callback);
    },

    getCurrentCoferenceStatus: function (callback) {
        server.doRequest("/Conference/GetCurrentConferenceStatus", "POST", "", callback);
    },

    startNewConference: function (callback) {
        server.doRequest("/Conference/StartNewConference", "POST", "", callback);
    },

    getSignInContent: function (callback) {
        server.doRequest("/Common/GetSignInArea", "GET", "", callback);
    },

    removeUserFromRole: function (userId, roleName, callback) {
        var params = {
            user_id: userId,
            role_name: roleName
        };
        server.doRequest("/AdminPanel/RemoveUserFromRole", "POST", params, callback);
    },

    addUserToRole: function (userId, roleName, callback) {
        var params = {
            user_id: userId,
            role_name: roleName
        };
        server.doRequest("/AdminPanel/AddUserToRole", "POST", params, callback);
    },

    getRolesInfo: function (userId, callback) {
        var params = {
            user_id: userId
        };
        server.doRequest("/AdminPanel/GetRolesInfo", "POST", params, callback);
    },

    doRequest: function (url, requestType, params, callback) {
        $.ajax({
            url: url,
            beforeSend: function () {
                requestsLaunched++;
                $(".spinner").show();
            },
            complete: function () {
                requestsLaunched--;
                if (requestsLaunched === 0) {
                    $(".spinner").hide();
                }
            },
            data: params,
            dataType: "json",
            type: requestType,
            success: function (data) {
                debugger;
                callback(data);
            },
            error: function (error) {
                if (error.status == 200) {
                    callback(error.responseText);
                } else {
                    alert(JSON.parse(error.responseText));
                }
            }
        });
    }
};