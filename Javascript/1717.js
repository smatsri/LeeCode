const log = (...args) => console.log(...args);

const maximumGain = function (s, x, y) {
  let [a, b, max, min] = x < y ? ['a', 'b', y, x] : ['b', 'a', x, y]
  let arr = Array.from(s)

  let sum = 0;
  for (let i = arr.length - 1; i >= 0; i--) {

    if (arr[i] !== b || arr[i + 1] !== a) {
      continue;
    }


    sum += max
    arr.splice(i, 2)

  }

  for (let i = arr.length - 1; i >= 0; i--) {

    if (arr[i] !== a || arr[i + 1] !== b) {
      continue;
    }


    sum += min
    arr.splice(i, 2)

  }


  return sum
};

const run = ([input, x, y], expected) => {
  const result = maximumGain(input, x, y);

  if (result === expected) {
    console.log('success', [input, x, y])
  } else {
    console.log('fail: expected: ' + expected + ' but got: ' + result)
  }

}

run(['bbaaabab', 1, 10], 31)
run(['bbaaabab', 10, 1], 22)
run(['cdbcbbaaabab', 4, 5], 19)



