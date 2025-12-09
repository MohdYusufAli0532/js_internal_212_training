let a=132;
let b=22;
let c=34;

let max;
if(a>=b && a>=c){
    max=a;
}
else if(b>=a && b>=c){
    max=b;
}
else{
    max=c;
}

console.log("Max element is:",max);