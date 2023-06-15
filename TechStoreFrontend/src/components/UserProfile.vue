<script setup>
import { reactive, computed } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { alpha, email, required, sameAs } from "@vuelidate/validators";
import BaseInput from "../components/BaseInput.vue";
import HiddenInput from "../components/HiddenInput.vue";

const initialState = {
  firstName: "",
  lastName: "",
  phone: "",
};

const initialEmailState = {
  email: "",
};

const emailState = reactive({
  ...initialEmailState,
});

const emailRules = {
  email: { required, email },
};

const initialPasswordState = {
  password: "",
  newPassword: "",
  confirmPassword: "",
};

const passwordState = reactive({
  ...initialPasswordState,
});

const passwordRules = {
  password: { required },
  newPassword: { required },
  confirmPassword: { required, sameAs: sameAs(computed(() => passwordState.newPassword)) },
};

const state = reactive({
  ...initialState,
});

const rules = {
  firstName: { required, alpha },
  lastName: { required, alpha },
  phone: { required },
};

const v$ = useVuelidate(rules, state);
const ve$ = useVuelidate(emailRules, emailState);
const vp$ = useVuelidate(passwordRules, passwordState);

async function updatePassword() {
  const isFormValid = await vp$.value.$validate();

  if (!isFormValid) {
    alert("Not Submitted!");
    return;
  }
  alert("Submitted!");
  clearForm(vp$, initialPasswordState, passwordState);
}

// TODO: make this a utils function
const clearForm = (form, initialFormState, formState) => {
  form.value.$reset();

  for (const [key, value] of Object.entries(initialFormState)) {
    formState[key] = value;
  }
};
</script>

<template>
  <v-container class="main pa-10 rounded-lg">
    <v-row>
      <h2>My Details</h2>
    </v-row>
    <v-row>
      <h3 class="pt-5 pb-1">Personal Information</h3>
      <v-divider color="warning"></v-divider>
      <v-col cols="4">
        Lorem ipsum, dolor sit amet consectetur adipisicing elit. Laudantium nemo corporis fugiat.
        Quasi, similique ipsum.
      </v-col>
      <v-col>
        <form class="pt-4">
          <v-row>
            <BaseInput class="mr-2" v-model="state.firstName" label="First Name" :v$="v$" />
            <BaseInput v-model="state.lastName" label="Last Name" :v$="v$" />
          </v-row>
          <v-row>
            <BaseInput v-model="state.phone" label="Phone Number" :v$="v$" />
          </v-row>
          <v-row>
            <v-btn class="my-4 text-capitalize w-25 btn" @click="v$.$validate"> Save </v-btn>
          </v-row>
        </form>
      </v-col>
    </v-row>
    <v-row>
      <h3 class="pt-5 pb-1">E-mail Address</h3>
      <v-divider color="warning"></v-divider>
      <v-col cols="4">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col>
        <form class="pt-2">
          <BaseInput v-model="emailState.email" label="E-mail Address" :v$="ve$.email" />
          <v-btn class="my-4 text-capitalize w-25 btn" @click="ve$.$validate"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
    <v-row>
      <h3 class="pt-5 pb-1">Password</h3>
      <v-divider color="warning"></v-divider>
      <v-col cols="4">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col>
        <form class="pt-2">
          <HiddenInput
            v-model="passwordState.password"
            label="Current Password"
            :v$="vp$.password"
          />
          <HiddenInput
            v-model="passwordState.newPassword"
            label="New Password"
            :v$="vp$.newPassword"
          />
          <HiddenInput
            v-model="passwordState.confirmPassword"
            label="Confirm Password"
            :v$="vp$.confirmPassword"
          />
          <v-btn class="my-4 text-capitalize w-25 btn" @click="updatePassword"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.main {
  background-color: var(--ts-c-primary-soft);
}

.btn {
  color: var(--ts-c-text-dark);
  background-color: var(--ts-c-ternary);
  font-weight: bold;
}
</style>
