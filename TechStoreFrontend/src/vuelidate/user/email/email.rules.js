import { required, email } from "@vuelidate/validators";

const emailRules = {
  email: { required, email },
};

export { emailRules };
