(function ($) {
    "use strict";

    // variables
    var wHight= $(window).height(),
        wWidth= $(window).width(),
        acHight = (wHight - 120),
        mainSlider = $('#main-slider');

    if(mainSlider.length){
        if(wWidth < 768){
            
            acHight = wWidth * ((8 - (wWidth - 300) / (467/3)) / 10);
            setTimeout(function(){
                var cameraContent = $('.cameraContent'),
                    mainSliderTop = (acHight - cameraContent.height())/2;
                cameraContent.css('top', mainSliderTop + 'px');
            }, 1000);
            
        }
        mainSlider.camera({
            height: acHight + 'px',
            loader: false,
            navigation: true,
            autoPlay:false,
            time: 4000,
            playPause: false,
            pagination: false,
            thumbnails: false
        });
    }
    
    //Partners carousel
    $('.partners').owlCarousel({
        loop:true,
        margin:10,
        nav:false,
        autoplay: true,
        autoplayTimeout: 1000,
        autoplaySpeed:2000,
        smartSpeed: 2000,
        dots: false,
        responsiveClass:true,
        responsive:{
            0:{
                items:1
            },
            479:{
                items:2
            },
            600:{
                items:3
            },
            991:{
                items:4
            },
            1000:{
                items:5
            }
        }
    });

    //Deal carousel
    var specialDealCarousel = $('.special-deal-content-carousel');
    specialDealCarousel.owlCarousel({
        loop:true,
        margin:10,
        nav:false,
        autoplay: false,
        autoplayTimeout: 1000,
        autoplaySpeed:2000,
        smartSpeed: 2000,
        dots: true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1
            },
            479:{
                items:1
            },
            600:{
                items:1
            },
            991:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });
    var dealImageCarousel = $('.deal-image-carousel');
    dealImageCarousel.owlCarousel({
        loop:true,
        margin:10,
        nav:false,
        autoplay: false,
        autoplayTimeout: 1000,
        autoplaySpeed:2000,
        smartSpeed: 2000,
        dots: true,
        responsiveClass:true,
        responsive:{
            0:{
                items:1
            },
            479:{
                items:1
            },
            600:{
                items:1
            },
            991:{
                items:1
            },
            1000:{
                items:1
            }
        }
    });

    specialDealCarousel.on('translate.owl.carousel', function (property) {
        dealImageCarousel.find('.owl-dot:eq(' + property.page.index + ')').click();
    });
    dealImageCarousel.on('translate.owl.carousel', function (property) {
        specialDealCarousel.find('.owl-dot:eq(' + property.page.index + ')').click();
    });

    //Contdown Page Installing 
    $('.countdown-timer').countdown('2016/12/25', function(event) {
        $(this).html(event.strftime('<ul><li>%D<span>Days</span></li><li>%H<span>Hours</span></li><li>%M<span>Mins</span></li><li>%S<span>Secs</span></li><ul>'));
    });


    //venobox
    if(wWidth > 767){
        $('.venoboxinline').venobox({
            framewidth: 'auto',        // default: ''
            frameheight: 'auto',    
            border: '10px',             // default: '0'
            bgcolor: '#fff',         // default: '#fff'
            titleattr: 'data-title',    // default: 'title'
            numeratio: true,            // default: false
            infinigall: true            // default: false
        });
    }
    
    //Quentity slider
    function productQuantity(){
        $('.up').on('click', function(){
            var this_select = $(this).siblings('input');
            this_select.val(parseInt(this_select.val())+1,10);
        });

        $('.down').on('click',function(){
            var this_select = $(this).siblings('input');
            this_select.val(parseInt(this_select.val())-1,10);
        });
    }
    productQuantity();
    $('a.venoboxinline').on('click',function(){
        setTimeout(productQuantity, 500);   
    });



    //Mean menu Installing for Mobile Menu
    $('nav#dropdown').meanmenu();

    $('.mobile-menu-area .mean-bar').prepend('<a href="index.html"><img alt="logo" src="img/logo/logo1.png"></a>');

    //Price Range Slider
    $(function() {
        $('#slider-range').slider({
            range: true,
            min: 0,
            max: 70000,
            values: [ 30000, 50000 ],
            slide: function( event, ui ) {
               $('#amount').val( "$" +  ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
            }
        });
        $('#amount').val( "$" + $( "#slider-range" ).slider( "values", 0 ) +
            " - $" + $( "#slider-range" ).slider( "values", 1 ) );
    });

    //
     $('.product-categories-list > ul > li > a').on('click', function(event){
        $(this).parent('li').toggleClass('categoryShow categoryHide');
        event.preventDefault();
    });

    $('.product-slider').owlCarousel({
        loop:true,
        margin:30,
        autoplaySpeed:1200,
        smartSpeed: 1200,
        nav:true,
        dots: false,
        responsive:{
            0:{
                items:1
            },
            600:{
                items:3
            },
            1000:{
                items:4
            }
        }
    })

    $('.navigation-advertisement-carousel').owlCarousel({
        animateOut: 'slideOutDown',
        animateIn: 'zoomIn',
        items:1,
        loop:true,
        margin:0,
        autoplayTimeout: 1200,
        autoplaySpeed:1200,
        smartSpeed: 1200,
        stagePadding:0,
        autoplay: true,
        nav:false,
        dots: false
    });
    // product page details advertisement
    $('.mega-sell-advertisement-carousel').owlCarousel({
        animateOut: 'slideOutDown',
        animateIn: 'fadeIn',
        items:1,
        loop:true,
        margin:0,
        autoplayTimeout: 1200,
        autoplaySpeed:1200,
        smartSpeed: 1200,
        stagePadding:0,
        autoplay: true,
        nav:false,
        dots: false
    });
    
    jQuery(window).load(function(){
        //Preloader
        $('.preloader').fadeOut('slow');
    });

})(jQuery);