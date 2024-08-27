import { required, email, alpha } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  email: { required, email },
  shippingAddress: { required },
  city: { required },
  zipCode: { required },
  country: { required },
  contactNumber: { required },
  paymentMethod: { required },
};

export { rules };
