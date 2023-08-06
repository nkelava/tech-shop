<script setup>
import { computed, reactive, ref } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { email } from "@vuelidate/validators";
import BaseInput from "@/components/common/BaseInput.vue";
import BaseAlert from "@/components/common/BaseAlert.vue";

const showAlert = ref(false);
const initialState = {
  email: "",
};

const rules = {
  email: { email },
};

const state = reactive({
  ...initialState,
});

const v$ = useVuelidate(rules, state);
const isFieldEmpty = computed(() => state.email.length < 1);

async function onSubscribe() {
  const isValid = await v$.value.$validate();

  if (!isValid) {
    return;
  }

  toggleAlert();
  clearForm();
}

function clearForm() {
  v$.value.$reset();

  for (const [key, value] of Object.entries(initialState)) {
    state[key] = value;
  }
}

function toggleAlert() {
  showAlert.value = !showAlert.value;
}
</script>

<template>
  <div class="newsletters ts-container">
    <div class="newsletters__heading">
      <h2>Subscribe to our newsletters!</h2>
      <h4>Get early access to new tech products and sales.</h4>
    </div>
    <div class="newsletters__subscribe">
      <base-input
        v-model="state.email"
        class="subscribe__input"
        label="Email"
        :v$="v$.email"
        variant="filled"
        density="compact"
      />

      <input type="submit" value="Subscribe" @click="onSubscribe" :disabled="isFieldEmpty" />
      <base-alert
        v-if="showAlert"
        type="success"
        title="Success!"
        message="Welcome to Tech Planet. Thank you for subscribing."
        @toggleShowAlert="toggleAlert"
      />
    </div>
  </div>
</template>

<style scoped>
.newsletters {
  display: grid;
  gap: 5rem;
  padding-top: 2rem;
  padding-bottom: 2rem;
  grid-template-areas: "heading input";
  background-color: var(--ts-c-bg-highlight);
}

.newsletters:nth-child(1) {
  grid-area: heading;
}

.newsletters:nth-child(2) {
  grid-area: input;
}

.newsletters__heading {
  color: var(--ts-c-text-dark);
  text-align: end;
}

.newsletters__heading h4 {
  color: var(--ts-c-primary-soft);
}

.newsletters__subscribe {
  position: relative;
  display: flex;
  align-items: start;
  align-self: center;
  justify-self: start;
}

.subscribe__input {
  width: 40em;
  margin-bottom: 0 !important;
  background-color: var(--ts-c-bg-light) !important;
  color: var(--ts-c-text-dark);
  border-radius: 5px;
  font-weight: bold;
}

input[type="submit"] {
  background-color: var(--ts-c-bg-dark);
  border-radius: 5px;
  color: var(--ts-c-text-light);
  font-weight: bold;
  height: 2rem;
  width: 7rem;
  position: absolute;
  right: 5px;
  margin-top: 5px;
}

input[type="submit"]:hover {
  background-color: var(--ts-c-primary-dark);
}

@media only screen and (max-width: 1024px) {
  .newsletters {
    grid-template-areas:
      "heading"
      "input";
    gap: 1rem;
    justify-content: center;
    align-items: center;
  }

  .newsletters__heading {
    text-align: center;
  }

  .newsletters__subscribe,
  .newsletters__subscribe .subscribe__input {
    width: 100%;
  }
}
</style>
