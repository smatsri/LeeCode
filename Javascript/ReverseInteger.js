const max = 2147483647
var reverse = function (x) {
  let result = 0
  let aa = x > 0 ? [1, x] : [-1, -1 * x]
  const mult = aa[0]
  let cur = aa[1]

  var d = Math.floor(Math.log10(cur)) + 1
  for (let i = 0; i < d; i++) {
    const r = cur % 10
    result += r * Math.pow(10, d - i - 1)
    cur = (cur - r) / 10
  }

  if (result > max) {
    return 0
  }
  return result * mult
};

y = reverse(1534236469)
console.log(y);