// Scrolling btn
let scrollBtn = document.querySelector(".scroll-btn");

window.onscroll = function () {
  if (window.scrollY >= 500) {
    scrollBtn.style.display = "block";
  }
  else {
    scrollBtn.style.display = "none"
  }
}

scrollBtn.onclick = function () {
  window.scrollTo({
    left: 0,
    top: 0,
    behavior: "smooth",
  });
};


// home swiper 
var swiper = new Swiper(".home-slider", {
  grabCursor: true,
  loop: true,
  centeredSlides: true,
  navigation: {
    nextEl: ".swiper-button-next",
    prevEl: ".swiper-button-prev"
  },
  autoplay:
  {
    delay: 3500,
  },
});



// Canvaas .........

window.onload = function () {
  var can = document.getElementById('canvas'),
    spanProcent = document.getElementById('procent'),
    c = can.getContext('2d');

  var posX = can.width / 2,
    posY = can.height / 2,
    fps = 1000 / 150,
    // procent = 0,
    oneProcent = 360 / 100,
    result = oneProcent * 64;

  c.lineCap = 'round';
  arcMove();

  function arcMove() {
    var deegres = 0;
    var acrInterval = setInterval(function () {
      deegres += 1;
      c.clearRect(0, 0, can.width, can.height);
      procent = deegres / oneProcent;

      spanProcent.innerHTML = procent.toFixed();

      c.beginPath();
      c.arc(posX, posY, 70, (Math.PI / 180) * 270, (Math.PI / 180) * (270 + 360));
      c.strokeStyle = '#b1b1b1';
      c.lineWidth = '5';
      c.stroke();

      c.beginPath();
      c.strokeStyle = '#3949AB';
      c.lineWidth = '5';
      c.arc(posX, posY, 70, (Math.PI / 180) * 270, (Math.PI / 180) * (270 + deegres));
      c.stroke();
      if (deegres >= result) clearInterval(acrInterval);
    }, fps);

  }


}


  // GRID / LIST VIEW (Shop.html)-----------------               -----------------------------     
  
  $(document).ready(function () {
    $('#list').click(function (event) { event.preventDefault(); $('#products .element').addClass('menu-element'); });
    $('#grid').click(function (event) { event.preventDefault(); $('#products .element').removeClass('menu-element'); $('#products .element').addClass('grid-group-element'); });
  });





// Counter ------------- ------------- -----------

$('.counting').each(function() {
  var $this = $(this),
      countTo = $this.attr('data-count');
  
  $({ countNum: $this.text()}).animate({
    countNum: countTo
  },

  {

    duration: 3000,
    easing:'linear',
    step: function() {
      $this.text(Math.floor(this.countNum));
    },
    complete: function() {
      $this.text(this.countNum);
      //alert('finished');
    }

  });  
  

});








// SKILLS  ................


  var gage = {
init: function(){
  // restart used for demo purposes - change to $('.gage').each(function(i){
$('.chart span').css({"width" : "0"}).parent().each(function(i){
  // Loop through .gage elements
  $('p', this).html($(this).attr("data-label"));
  // Set p html value to the data-label attr set in the element
  var timeout = parseInt(i) * 60 + 1100;
  // Set a timeout based on the iteration multiplied by 60 (will affect delay between animations) 
  $('span', this).delay(timeout).animate({"opacity" : "1"}, 0, function(){
    //Delay  
    $(this).css({"width" : $(this).parent().attr("data-level") + "%"});
  });
});
}
}

$(document).ready(function(){
// Call gage init function
gage.init();
// Interval used for demo purposes - remove if using  
setInterval(function() {
  gage.init();
}, 25000);
});



// Donate.........
document.getElementById("bloodTypeForm").addEventListener("submit", function(event) {
  event.preventDefault();
  var bloodType = document.getElementById("bloodType").value;
  // Here you can perform AJAX request to fetch hospitals for the given blood type
  // For simplicity, I'm just populating a static list-
  var hospitals = getHospitals(bloodType);
  displayHospitals(hospitals);
});

// Dummy function to get hospitals, replace with actual data fetching logic
function getHospitals(bloodType) {
  // Dummy hospital data
  var hospitals = [
      { name: "Military Hospital", location: "Shebin Elkom"},
      { name: "teaching Hospital", location: "Shebin Elkom" },
      { name: "Elaraby  Hospital", location: "Ashmon City" },
      { name: "Dar Al Fouad Hospital", location: "6 October City" }
  ];
  return hospitals;
}

// Function to display hospitals
function displayHospitals(hospitals) {
  var hospitalList = document.getElementById("hospitalList");
  hospitalList.innerHTML = ""; // Clear previous list
  hospitals.forEach(function(hospital) {
      var hospitalElement = document.createElement("div");
      hospitalElement.classList.add("hospital");
      hospitalElement.textContent = hospital.name + " - " + hospital.location;
      hospitalList.appendChild(hospitalElement);
  });
}
