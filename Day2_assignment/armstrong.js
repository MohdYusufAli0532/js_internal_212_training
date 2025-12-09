function isArmstrong(n) {
    let copy = n;
    let digits = n.toString().length; 
    let sum = 0;

    while (n !== 0) {
        let a = n % 10;
        sum += Math.pow(a, digits);
        n = Math.floor(n / 10);
    }

    return sum === copy;
}

console.log(isArmstrong(153));  
console.log(isArmstrong(370));   
console.log(isArmstrong(371));     
console.log(isArmstrong(9474));  
console.log(isArmstrong(123));   
