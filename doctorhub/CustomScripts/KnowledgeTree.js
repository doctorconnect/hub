function FilterResults() {
    var categories = [];
    $.each($("input[name='category']:checked"), function () {
        categories.push($(this)[0].id);
    });
    var doctypes = [];
    $.each($("input[name='doctype']:checked"), function () {
        doctypes.push($(this)[0].id);
    });
    var lobs = [];
    $.each($("input[name='lob']:checked"), function () {
        lobs.push($(this)[0].id);
    });
    window.location.href = "/KnowledgeTree/KnowledgeTree?categories=" + categories + "&doctypes=" + doctypes + "&lobs=" + lobs;
}




