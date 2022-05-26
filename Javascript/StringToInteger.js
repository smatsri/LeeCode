const minus = 45
const plus = 43
const max = 2147483647
const min = -2147483648

const myAtoi = function (input) {
  let mult = 1
  let arr = []
  let b = false
  let signSet = false
  for (let i = 0; i < input.length; i++) {
    let c = input.charCodeAt(i)

    if (c == minus || c == plus) {
      if(b){}
      if(signSet) return 0
      else if(b) {
        break
      }
      mult = c == plus ? 1 : -1;
      signSet = true;
    } else if (c == plus) {
      signSet = true;
    }
    else if (c == 32) {
      if (b) {
        break
      } else {
        continue
      }
    }
    else {
      c -= 48
      if (c > 9 || c < 0) {
        if (b) {
          break;
        } else {
          return 0
        }
        
      } else {
        arr.push(c)
        b = true
      }
    }
  }

  let result = 0
  let n = arr.pop()
  let p = 0
  while (n != undefined) {
    result += n*Math.pow(10,p)
    if (result > max) {
      break
    } 
    p++
    n = arr.pop()
  }


  result *= mult
  if (result > max) {
    return max
  }
  if (result < min) {
    return min
  }
  return result;
};

x = myAtoi("-5-")

console.log(x)

