function LoadPostsAndComment() {
    $.ajax
        ({
            url: "/Home/LoadPostsAndCommentPartialView",
            contentType: "application/html; charset=utf-8",
            type: "POST",
            datatype: "html",
            success: function (t) {
                $("#dvHomePostsAndCommentBind").html(t)
            },
            error: function () {
                $("#dvHomePostsAndCommentBind").html("Post Not Found");
                window.location.href = "/Home/Error";
            }
        })
}
function LoadDocumentHome() {
    $.ajax
        ({
            url: "/Home/LoadDocumentPartialView",
            contentType: "application/html; charset=utf-8",
            type: "POST",
            datatype: "html",
            success: function (t) {
                $("#dvHomeDocumentBind").html(t)
            },
            error: function () {
                $("#dvHomeDocumentBind").html("Document Not Found");
                window.location.href = "/Home/Error";
            }
        })
}
function LoadBlogeHome() {
    $.ajax
        ({
            url: "/Home/LoadBlogePartialView",
            contentType: "application/html; charset=utf-8",
            type: "POST",
            datatype: "html",
            success: function (t) {
                $("#dvBlogeBind").html(t)
            },
            error: function () {
                $("#dvBlogeBind").html("Blog Not Found");
                window.location.href = "/Home/Error";
            }
        })
}
