log("================================================")

const MAX = 2147483647
const MIN = -2147483648

const DigitType = Symbol("DigitType")
const WhitespaceType = Symbol("WhitespaceType")
const MinusType = Symbol("MinusType")
const PlusType = Symbol("PlusType")
const InvalidCharType = Symbol("InvalidCharType")

const Digit = value => ({ type: DigitType, value })
const Whitespace = ({ type: WhitespaceType })
const Minus = ({ type: MinusType })
const Plus = ({ type: PlusType })
const InvalidChar = ({ type: InvalidCharType })


const myAtoi = pipe(
  chars,
  map(mapToSymbol),
  skipWhile(eq(Whitespace)),
  takeWhile(s => s !== Whitespace && s!== InvalidChar),
  toArray,
  numReducer
)

log(myAtoi("-5-"))

function* chars(str = "") {
  for (let i = 0; i < str.length; i++) {
    yield str.charCodeAt(i)
  }
}

function mapToSymbol (code)  {
  switch (code) {
    case 32: return Whitespace;
    case 43: return Plus;
    case 45: return Minus;
  }
  if (code >= 48 && code <= 57) {
    return Digit(code - 48)
  }
  return InvalidChar;
}

function numReducer (arr = []) {
  var a = arr.reduce(
    ({ res, mult, fail: stop }, s) => {
      if (!stop) {
        const type = s.type
        if (type === MinusType || type === PlusType) {
          if (mult === undefined) {
            mult = type === MinusType ? -1 : 1
            res = 0
          } else {
            stop = true
          }
        } else {
          res = res * 10 + s.value
          if(mult === undefined) {
            mult = 1
          }
        }
      }
      return {
        res, mult, fail: stop
      }
    },
    { res: 0, mult: undefined, fail: false },
  )

  const res = (a.mult || 1) * a.res
  if (res > MAX) {
    return MAX
  }
  if (res < MIN) {
    return MIN
  }
  return res;
}

function match(fns = []) {
  return (input) => {
    if (!input) return
    const f = fns.find(x => x[0] === input.type)
    if (f) {
      return f[1](input.value)
    }
  }
}


function eq(x) {
  return y => x === y
}

function noteq(x) {
  return y => x !== y
}

function pipe(...fns) {
  return (input) => {
    for (const fn of fns) {
      input = fn(input);
    }
    return input
  }
}


function filter(fn) {
  return function* (seq) {
    for (const item of seq) {
      if (fn(item)) {
        yield item
      }
    }
  }
}

function map(fn) {
  return function* (seq) {

    for (const item of seq) {
      yield fn(item)
    }
  }
}

function skipWhile(fn) {
  return function* (seq) {
    let b = false
    for (const item of seq) {
      if (!b) {
        b = !fn(item)
        if (b) {
          yield item
        }
      } else {
        yield item
      }
    }
  }
}

function take(count = 0) {
  return function* (seq) {
    let cur = 0;
    for (const item of seq) {
      if (cur < count) {
        yield item
      } else {
        break
      }
      cur++;
    }
  }
}

function takeWhile(fn) {
  return function* (seq) {
    for (const item of seq) {
      if (fn(item)) {
        yield item
      } else {
        break;
      }
    }
  }
}

function reduce(fn, state) {
  return function* (seq) {
    let curState = state
    for (const item of seq) {
      curState = fn(curState, item)
      yield curState
    }
    return curState
  }
}

function log(...args) {
  console.log(...args)
  return args[0]
}

function toArray(seq) {
  const arr = []
  for (const item of seq) {
    arr.push(item)
  }
  return arr
}

/**
 * const toString = match([
  [DigitType, d => `digit (${d})`],
  [WhitespaceType, _ => "whitespace"],
  [MinusType, _ => "minus sign"],
  [PlusType, _ => "plus sign"],
  [InvalidCharType, _ => "invalid character"]
]
 */