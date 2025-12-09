let a = 20;
let b = 5;
let choice = "mul"; 

switch (choice) {
    case "add":
        console.log("Addition =", a + b);
        break;

    case "sub":
        console.log("Subtraction =", a - b);
        break;

    case "mul":
        console.log("Multiplication =", a * b);
        break;

    case "divide":
        if (b !== 0) {
            console.log("Division =", a / b);
        } else {
            console.log("Cannot divide by zero");
        }
        break;

    default:
        console.log("Invalid choice");
}
