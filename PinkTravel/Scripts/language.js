var mySiteNamespace = {};

mySiteNamespace.switchLanguage = function(lang) {
    $.cookie('language', lang);
    window.location.reload();
};

$(document).ready(function() {
    $('#lang-en').click(function () { mySiteNamespace.switchLanguage('en'); });
    $('#lang-ro').click(function () { mySiteNamespace.switchLanguage('ro'); });
});