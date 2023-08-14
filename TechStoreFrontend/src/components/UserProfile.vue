<script setup>
import { reactive, computed } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { alpha, email, required, sameAs } from "@vuelidate/validators";
import BaseInput from "@/components/common/BaseInput.vue";
import HiddenInput from "@/components/common/BaseInputHidden.vue";

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
  <v-container class="account-details pa-10 rounded-lg">
    <v-row>
      <h2>My Details</h2>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">Personal Information</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum, dolor sit amet consectetur adipisicing elit. Laudantium nemo corporis fugiat.
        Quasi, similique ipsum.
      </v-col>
      <v-col cols="12">
        <form class="details-item__form">
          <v-row>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="state.firstName" label="First Name" :v$="v$" />
            </v-col>
            <v-col class="pb-0" cols="12" lg="6">
              <base-input v-model="state.lastName" label="Last Name" :v$="v$" />
            </v-col>
            <v-col class="pt-0" cols="12">
              <base-input v-model="state.phone" label="Phone Number" :v$="v$" />
              <v-btn class="details-item__btn" @click="v$.$validate"> Save </v-btn>
            </v-col>
          </v-row>
        </form>
      </v-col>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">E-mail Address</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col cols="12">
        <form class="details-item__form">
          <base-input v-model="emailState.email" label="E-mail Address" :v$="ve$.email" />
          <v-btn class="details-item__btn" @click="ve$.$validate"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
    <v-row class="details-item">
      <h3 class="details-item__title">Password</h3>
      <v-divider color="warning"></v-divider>
      <v-col class="details-item__desc" cols="12">
        Lorem ipsum dolor sit amet consectetur adipisicing elit. Ab ipsum beatae inventore, suscipit
        sequi rerum aperiam, dicta commodi velit fugit perferendis delectus odio consequatur et!
        Vitae ipsam adipisci animi iure.
      </v-col>
      <v-col>
        <form class="details-item__form">
          <hidden-input
            v-model="passwordState.password"
            label="Current Password"
            :v$="vp$.password"
          />
          <hidden-input
            v-model="passwordState.newPassword"
            label="New Password"
            :v$="vp$.newPassword"
          />
          <hidden-input
            v-model="passwordState.confirmPassword"
            label="Confirm Password"
            :v$="vp$.confirmPassword"
          />
          <v-btn class="details-item__btn" @click="updatePassword"> Save </v-btn>
        </form>
      </v-col>
    </v-row>
  </v-container>
</template>

<style scoped>
.account-details {
  background-color: var(--ts-c-primary-soft);
  max-width: 1200px;
}

.details-item {
  margin-bottom: 20px;
}

.details-item__title {
  padding-bottom: 5px;
}

.details-item__title:first-child {
  margin-top: 30px;
}

.details-item__desc {
  opacity: 0.8;
}

.details-item__form {
  margin-top: 10px;
}

.details-item__btn {
  width: 100%;
  max-width: 250px;
  font-weight: bold;
  text-transform: capitalize;
  color: var(--ts-c-text-dark);
  background-color: var(--ts-c-ternary);
}

::v-deep .v-divider {
  margin-bottom: 1rem;
}
</style>
