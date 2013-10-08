var armorTokenManager = armorTokenManager || {
    setArmorToken: function(xhr, control) {
        var armorToken = xhr.getResponseHeader("ARMOR");

        $(control).val(armorToken);
        ajaxManager.setHeader(armorToken);
    }
}