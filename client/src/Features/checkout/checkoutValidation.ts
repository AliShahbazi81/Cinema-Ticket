import * as yup from "yup";

export const validationSchema = [
  // First object will be used for our CheckOut Form
  yup.object({
    fullName: yup.string().required("Full name is required"),
    address1: yup.string().required("Address line 1 is required"),
    address2: yup.string().required(),
    city: yup.string().required(),
    state: yup.string().required(),
    zip: yup.string().required(),
    country: yup.string().required(),
  }),
  // Since we do not have any validation in our review form, this would be empty
  yup.object(),
  // This will be used for our payment form
  yup.object({
    nameOnCard: yup.string().required(),
  }),
];
