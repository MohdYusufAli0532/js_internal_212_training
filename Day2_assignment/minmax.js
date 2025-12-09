function GetMinMax(arr)
{
    let min=arr[0];
    let max=arr[0];


    for(let i=0;i<arr.length;i++){
        if(arr[i]<min){
            min=arr[i];
        }
        if(arr[i]>max){
            max=arr[i];
        }
    }
    return {min,max};
}
console.log(GetMinMax([1,2,4,5,23,54,75,4353,234]));