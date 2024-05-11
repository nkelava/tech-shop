import { required, alpha } from "@vuelidate/validators";

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  shippingAddress: { required },
  city: { required },
  zipCode: { required },
  country: { required },
  contactNumber: { required },
};

export { rules };
