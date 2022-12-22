import { FormGroup, FormControlLabel, Checkbox } from "@mui/material";
import { useState } from "react";

interface Props {
  items: string[];
  checked?: string[];
  onChange: (items: string[]) => void;
}

export default function CheckBoxButtons({ items, checked, onChange }: Props) {
  const [checkedItems, setChecedkItems] = useState(checked || []);

  // !This function is to handle the checkbox
  function handleCheck(value: string) {
    // The purpose of this function is to check if the value is already in the array
    const currentIndex = checkedItems.findIndex((item) => item === value);
    let newChecked: string[] = [];

    // currenctIndex === -1 means that the value is not in the array => user has not selected the checkbox
    if (currentIndex === -1) newChecked = [...checkedItems, value];
    // If the value is in the array, we need to remove it => user has selected the checkbox
    // filter() is to return a new array with the items that meet the condition => which means the items that user has not selected
    else newChecked = checkedItems.filter((item) => item !== value);

    // !REMEMBER: We need to update the state
    setChecedkItems(newChecked);
    onChange(newChecked);
  }

  return (
    <FormGroup>
      {items.map((items) => (
        <FormControlLabel
          control={
            <Checkbox
              checked={checkedItems.indexOf(items) !== -1}
              onClick={() => handleCheck(items)}
            />
          }
          label={items}
          key={items}
        />
      ))}
    </FormGroup>
  );
}
