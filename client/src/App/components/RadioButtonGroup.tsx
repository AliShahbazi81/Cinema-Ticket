import {
  FormControl,
  RadioGroup,
  FormControlLabel,
  Radio,
} from "@mui/material";

// The purpose of this interface is to define the type of the props
interface Props {
  options: any[];
  onChange: (event: any) => void;
  selectedValue: string;
}

// The purpose of this function is to render a radio button group
// !REMEMBER: Since we are make this component reusable, we need to pass in the props
export default function RadioButtonGroup({
  options,
  onChange,
  selectedValue,
}: Props) {
  return (
    <FormControl component="fieldset">
      <RadioGroup onChange={onChange} value={selectedValue}>
        {options.map(({ value, label }) => (
          <FormControlLabel
            value={value}
            control={<Radio />}
            label={label}
            key={value}
          ></FormControlLabel>
        ))}
      </RadioGroup>
    </FormControl>
  );
}
