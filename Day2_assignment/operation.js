function calculate(a, b, operator) {
    switch(operator) {
        case '+':
            return a + b;
        case '-':
            return a - b;
        case '*':
            return a * b;
        case '/':
            return b !== 0 ? a / b : "Division by zero error";
        case '%':
            return b !== 0 ? a % b : "Modulo by zero error";
        default:
            return "Invalid operator";
    }
}

console.log(calculate(10, 5, '+')); // 15
console.log(calculate(10, 5, '/')); // 2
console.log(calculate(10, 0, '/')); // Division by zero error
