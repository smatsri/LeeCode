log("================================================")
function log(...args) {
  console.log(...args)
  return args[0]
}

log(myAtoi("   -42"))
log(myAtoi("   +0 123"))
log(myAtoi("4193 with words"))


function myAtoi(str) {
  const MAX = 2147483647
  const MIN = -2147483648
  let res = 0, mult, init = false;
  for (let i = 0; i < str.length; i++) {
    const char = str[i]

    if (char === ' ') {
      if (init) break;
      else continue
    } else if (char === '+' || char === '-') {
      init = true;
      if (mult === undefined) {
        mult = char === '-' ? -1 : 1
        res = 0
      } else {
        break
      }
    } else {
      if(char >= '0' && char  <= '9'){
        init = true;
        res = res * 10 + (+char)
        if (mult === undefined) {
          mult = 1
        }
      } else {
        break
      }
      }
      const n = parseInt(char)
      if (!isNaN(n)) {
       
    }

  }
  res = (mult || 1) * res
  if (res > MAX) {
    return MAX
  }
  if (res < MIN) {
    return MIN
  }
  return res;
}