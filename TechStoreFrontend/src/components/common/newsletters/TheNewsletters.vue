<script setup>
import { reactive } from "vue";
import { useVuelidate } from "@vuelidate/core";
import { email, required } from "@vuelidate/validators";
import BaseInput from "@/components/BaseInput.vue";

const initialState = {
  email: "",
};

const rules = {
  email: { email, required },
};

const state = reactive({
  ...initialState,
});

const v$ = useVuelidate(rules, state);

async function onSubscribe() {
  const isValid = await v$.value.$validate();

  if (!isValid) {
    return;
  }

  clearForm();
}

function clearForm() {
  v$.value.$reset();

  for (const [key, value] of Object.entries(initialState)) {
    state[key] = value;
  }
}
</script>

<template>
  <div class="newsletters">
    <div class="newsletters__heading">
      <h1>Subscribe to our newsletter!</h1>
      <h3>Get early access to new tech products and sales.</h3>
    </div>
    <div class="newsletters__subscribe">
      <base-input
        v-model="state.email"
        class="subscribe__input"
        label="Email"
        :v$="v$.email"
        density="compact"
      />
      <input type="submit" value="Subscribe" @click="onSubscribe" />
    </div>
  </div>
</template>

<style scoped>
.newsletters {
  background-color: var(--ts-c-bg-highlight);
  padding: 2rem;
  display: grid;
  gap: 5rem;
  grid-template-areas: "heading input";
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

.newsletters__heading h3 {
  color: var(--ts-c-primary-soft);
}

.newsletters__subscribe {
  position: relative;
  display: flex;
  align-items: center;
  align-self: center;
  justify-self: start;
}

.subscribe__input {
  background-color: var(--ts-c-bg-light) !important;
  border-radius: 5px;
  color: var(--ts-c-text-dark);
  font-weight: bold;
  width: 40em;
  margin-bottom: 0 !important;
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
}

input[type="submit"]:hover {
  background-color: var(--ts-c-primary-soft);
}

@media only screen and (max-width: 1000px) {
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
