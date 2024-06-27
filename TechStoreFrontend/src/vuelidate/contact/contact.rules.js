import { required, email, alpha } from "@vuelidate/validators";

const contactRules = {
  name: { required },
  email: { email, required },
  subject: { required },
  message: { required },
};

export { contactRules };
