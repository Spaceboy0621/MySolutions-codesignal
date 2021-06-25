function sudoku2(grid) {
  return (
    validateRows(grid) &&
    validateRows(rotateGrid(grid)) &&
    validateSubGrids(grid)
  );
}

// Validate each row.
function validateRows(grid) {
  let valid = true;

  for (const row of grid) {
    if (!valid) {
      break;
    }

    let dict = {};

    row.filter(item => item !== ".").forEach(item => {
      if (dict[item]) {
        valid = false;
      } else {
        dict[item] = 1;
      }
    });
  }

  return valid;
}

// Rotate the grid by 90deg. 
// This allows us to then validate the columns as rows.
function rotateGrid(grid) {
  return grid.map((inArr, i) => {
    const newArr = [];

    for (const arr of grid) {
      newArr.push(arr[i]);
    }

    return newArr.reverse();
  });
}

// Convert each 3x3 grid into a row of 9.
// This allows us to validate the sub grids as rows.
function validateSubGrids(grid) {
  const subGrids = [];
  const getSubGridRow = function(grid, curRow, curCol) {
    const currentSubGrid = [];

    for (let row = curRow; row < curRow + 3; row++) {
      for (let col = curCol; col < curCol + 3; col++) {
        currentSubGrid.push(grid[row][col]);
      }
    }

    return currentSubGrid;
  };

  for (let i = 0; i < grid.length; i += 3) {
    for (let j = 0; j < grid.length; j += 3) {
      subGrids.push(getSubGridRow(grid, i, j));
    }
  }

  return validateRows(subGrids);
}
