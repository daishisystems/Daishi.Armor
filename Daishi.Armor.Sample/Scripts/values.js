var values = values || {
    getAll: function(controlName) {
        $("#body").delegate(controlName, "click", function() {
            $.ajax({
                type: "GET",
                url: "../api/values",
                accepts: "application/json",
                contentType: "application/json",
                success: function(response, status, xhr) {
                    armorTokenManager.setArmorToken(xhr, "#armorToken");
                    var valueList = "Values:";

                    $.each(response, function(k, v) {
                        valueList += "\n" + v;
                    });

                    alert(valueList);
                },
                failure: function() {
                    alert("Something went wrong...");
                }
            });
        });
    }
}