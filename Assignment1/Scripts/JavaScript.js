$(document).ready(function () {
    $('#btnGoTo').prop('disabled', true);
    $("#SearchResult").prop('disabled', true);
    
    $(function () {
        $('#tweetButton').click(function (event) {
            event.preventDefault();
            var tweet = {
                message: $('#newTweet').val()
            };
            $.ajax({
                type: "POST",
                url: "/Home/newTweet",
                data: JSON.stringify(tweet),
                dataType: "html",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#newTweet').val('');
                    $('#div_GetTweets').html(data);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        });
    });

    $(function () {
        $('#btnSearchUser').click(function (event) {
            event.preventDefault();
            $.ajax({
                type: "POST",
                url: "/Home/Search",
                data: JSON.stringify({ id: $('#SearchUser').val() }),
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    $('#SearchResult').val(data);
                    $('#btnGoTo').prop('disabled', false);
                },
                error: function () {
                    $('#SearchResult').val('User not found!');
                    $('#btnGoTo').prop('disabled', true);
                    alert("Error Occured!")
                }
            });
        });
    });

    $(function () {
        $('#followUser').click(function (event) {
            event.preventDefault();
            $.ajax({
                type: "POST",
                url: "/Home/FollowUser",
                data: JSON.stringify({ id: $('#profileuser').val() }),
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if(data!="")
                    alert(data);
                },
                error: function () {
                    alert("Error occured!")
                }
            });
        });
    });

    $(function () {
        $('#unFollowUser').click(function (event) {
            event.preventDefault();
            $.ajax({
                type: "POST",
                url: "/Home/UnFollowUser",
                data: JSON.stringify({ id: $('#profileuser').val() }),
                dataType: "json",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    if (data != "")
                        alert(data);
                },
                error: function () {
                    alert("Error occured!")
                }
            });
        });
    });

    $(function () {
        $('.updateButton').click(function (event) {
            var rowId = this.id.split("_")[1];
            event.preventDefault();
            var tweet = {
                tweet_id: $('#tweetid_' + rowId).val(),
                message: $('#updatedMessage_' + rowId).val()
            };
            $.ajax({
                type: "POST",
                url: "/Home/updateTweet",
                data: JSON.stringify(tweet),
                dataType: "html",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert("The tweet is updated successfully!");
                    $('#div_SelfTweets').html(data);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        });
    });

    $(function () {
        $('.deleteButton').click(function (event) {
            var rowId = this.id.split("_")[1];
            event.preventDefault();
            var tweet = {
                tweet_id: $('#tweetid_' + rowId).val()
            };
            $.ajax({
                type: "POST",
                url: "/Home/deleteTweet",
                data: JSON.stringify(tweet),
                dataType: "html",
                contentType: 'application/json; charset=utf-8',
                success: function (data) {
                    alert("The tweet is deleted successfully!");
                    $('#div_SelfTweets').html(data);
                },
                error: function () {
                    alert("Error occured!!")
                }
            });
        });
    });

    $(function () {
        $('#btnGoTo').click(function (event) {
            event.preventDefault();
            window.location.href = '/Home/ProfilePage/' + $('#SearchResult').val();
        });
    });

});  