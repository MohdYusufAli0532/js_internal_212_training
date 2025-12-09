function checkVotingEligibility(age) {
    if (age >= 18) {
        return "Eligible to vote";
    } else {
        return "Not eligible to vote";
    }
}

console.log(checkVotingEligibility(20)); // Eligible to vote
console.log(checkVotingEligibility(16)); // Not eligible to vote
