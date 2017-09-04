$(document).ready(() => {
    // https://stackoverflow.com/questions/2507030/email-validation-using-jquery
  function isEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
  }; 
  
  // Begin Validation
  $("#signUpButton").on("click", (e) => {
    // First prevent the button from completing the event
    e.preventDefault();
    
    // Next, grab the email and validate it
    var emailValue = $("#newsletterEmailInput").val();
    if (isEmail(emailValue)) {
      // Send API Call to add to Newsletter list
      //...
      // Update the User with success if successful
      $("#validationResult").text("You have successfully subscribed to our newsletter!");
      $("#validationResult").removeClass("validationError");
      $("#validationResult").addClass("validationSuccess");
    }
    else {
      $("#validationResult").text("Please enter a valid email address");
      $("#validationResult").addClass("validationError");
      $("#validationResult").removeClass("validationSuccess");
    }
  });
});