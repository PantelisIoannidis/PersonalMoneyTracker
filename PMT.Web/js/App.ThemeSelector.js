var themeSelector = function () {

    var bootstrapLink;
    var customCssLink;

    function initThemeSelector() {
        applyThemeButtonEvent();
    };

    function applyThemeButtonEvent() {
        $(".applyTheme(invalid)").on("click", function (e) {
            e.preventDefault();
            SetPreferences();
        });
    }

    function SetPreferences() {
        var theme = $("#Theme").val();
        var itemsPerPage = $("#ItemsPerPage").val();
        var localization = $("#Localization").val();
        var userId = $("#UserId").val();
        var obj = {
            "Theme": theme,
            "ItemsPerPage": itemsPerPage,
            "Localization": localization,
        };
        var myJSON = JSON.stringify(obj);
        $.post(pmt.rootPath + "Settings/SaveSettings/",
            {
                preferences: myJSON,
                Theme: theme,
                ItemsPerPage: itemsPerPage,
                Localization: localization,
                UserId: userId
            },
            function (retURL) {
                window.location.reload(true);
            });
    }


    return {
        initThemeSelector: initThemeSelector,
    }
}();