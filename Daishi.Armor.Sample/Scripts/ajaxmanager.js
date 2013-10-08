var ajaxManager = ajaxManager || {
    setHeader: function(armorToken) {
        $.ajaxSetup({
            beforeSend: function(xhr) {
                xhr.setRequestHeader("Authorization", "ARMOR " + armorToken);
            }
        });
    }
};