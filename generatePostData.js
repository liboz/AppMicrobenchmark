arr = []
for (i = 0; i < 100; i++) {
    arr.push({index: i, name: String.fromCharCode(97 + (i % 26))})
}
console.log({users: arr})