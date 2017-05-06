window.PMT = {};

(function (cr) {
    var rootPath;
    cr.rootPath = rootPath;
}(window.PMT));

(function (cr) {
    var hookStageEvents = function () {
        var myBarCurrentLink = $("input[name='myBarSelector']").val();
        $(".my-bar  li").removeClass('active');
        $("#" + myBarCurrentLink).parent().addClass('active');

        var $transCurrentLink = $(".transTable > tbody > tr");
        $transCurrentLink.click(function (e) {
            $this = $(this);
            $transCurrentLink.siblings().removeClass('info');
            $this.addClass('info');

        });
    };
    cr.hookStageEvents = hookStageEvents;

}(window.PMT));