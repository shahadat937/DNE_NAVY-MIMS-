$.fn.extend(
{
    yearpicker: function () {
        var select = $(this);

        var year = new Date().getFullYear();
        for (var i = -10; i < 11; i++) {
            var option = $('<option/>');
            var year_to_add = year + i;

            option.val(year_to_add).text(year_to_add);

            if (year == year_to_add) {
                option
                  .css('font-weight', 'bold')
                  .attr('selected', 'selected');
            }

            select.append(option);
        }
    },
    halfyearpicker: function () {
        var select = $(this);

        var date = new Date();
        var year = date.getFullYear();
        var half = Math.floor(date.getMonth() / 6);

        for (var i = -10; i < 11; i++) {
            var year_to_add = year + i;

            for (var j = 0; j < 2; j++) {
                var option = $('<option/>');
                var half_text = j == 0 ? 'Jan-Jun' : 'Jul-Dec';
                var value = year_to_add + '-' + (j + 1);
                var text = year_to_add + ' ' + half_text;

                option.val(value).text(text);

                if (year_to_add == year && half == j) {
                    option
                      .css('font-weight', 'bold')
                      .attr('selected', 'selected');
                }

                select.append(option);
            }
        }
    },
    quarteryearpicker: function () {
        var select = $(this);

        var date = new Date();
        var year = date.getFullYear();
        var quarter = Math.floor(date.getMonth() / 3);

        for (var i = -10; i < 11; i++) {
            var year_to_add = year + i;

            for (var j = 0; j < 4; j++) {
                var option = $('<option/>');
                var quarter_text = get_quarter_text(j);

                var value = year_to_add + '-' + (j + 1);
                var text = year_to_add + ' ' + quarter_text;

                option.val(value).text(text);

                if (year_to_add == year && quarter == j) {
                    option
                      .css('font-weight', 'bold')
                      .attr('selected', 'selected');
                }

                select.append(option);
            }
        }

        function get_quarter_text(num) {
            switch (num) {
                case 0:
                    return 'Jan-Mar';
                case 1:
                    return 'Apr-Jun';
                case 2:
                    return 'Jul-Sep';
                case 3:
                    return 'Oct-Dec';
            }
        }
    }
});