
function fun(x)
{
    return 2*x;
}
let ch=fun(72);
console.log(ch);


let a = [1,2,3,"apple","banana",ch]; //here important is ch->144

console.log(a);


let fruit=["apple","banana","Guava","Orange","Kiwi"];

for(let i=0;i<fruit.length;i++){
    console.log(fruit[i]);
}

for(let i of fruit)
{
    console.log(i);
}

fruit.push("Papaya");
console.log(fruit);

fruit.pop();

console.log(fruit);

fruit.unshift("Mango");
console.log(fruit);

console.log(fruit.includes("Mango"));

console.log(fruit.indexOf("banana"));


//map()

let numbers=[1,2,3];
let doubled=numbers.map(n=>n*2);
console.log(numbers);
console.log(doubled);


//filter()

let nums=[10,25,30,40];

let result=nums.filter(n=>n>20);
console.log(result);

//reduce()
//works like aggregate function in sql

let total=nums.reduce((acc,val)=>acc+val,0);

console.log(total);



let arr=[2,4,6,8];

let new_arr=arr.map(n=>n*2);
console.log(new_arr);

let arr1=[10,25,30,5,60];

let greaters=arr1.filter(n=>n>20);

console.log(greaters);

let arr2=[1,2,3,4,5];
let res=arr2.reduce((acc,val)=>acc+val,0);

console.log(res);