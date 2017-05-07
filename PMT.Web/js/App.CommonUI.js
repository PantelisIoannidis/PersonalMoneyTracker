var commonUI = function(){
    var calculateActiveElement = function () {
        var myBarCurrentLink = $("input[name='myBarSelector']").val();
        $(".my-bar  li").removeClass('active');
        $("#" + myBarCurrentLink).parent().addClass('active');
    };
    return {
        calculateActiveElement: calculateActiveElement,
    };
}();
