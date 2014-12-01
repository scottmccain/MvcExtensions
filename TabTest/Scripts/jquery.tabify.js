jQuery.fn.Tabify = function () {
    console.log(this);

    var outerThis = this;
    var currentAttrValue = jQuery(this).find('ul li a').first('a').attr('href');

    jQuery(this).children('ul').addClass('tab-links');
    jQuery(this).find('ul li a').first('a').parent('li').addClass('active').siblings().removeClass('active');
    jQuery(this).find(currentAttrValue).show().siblings().hide();

    jQuery(this).find('ul li a').each(function () {
        var attrValue = jQuery(this).attr('href');
        console.log(attrValue);
    });

    jQuery(this).find('.tab-links a').on('click', function (e) {
        currentAttrValue = jQuery(this).attr('href');

        // Show/Hide Tabs
        jQuery(outerThis).find(currentAttrValue).show().siblings().hide();

        // Change/remove current tab to active
        jQuery(this).parent('li').addClass('active').siblings().removeClass('active');


        e.preventDefault();
    });
};

