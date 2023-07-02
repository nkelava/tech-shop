<script setup>
import { ref } from "vue";

const props = defineProps(["label", "v$"]);

const isInputHidden = ref(true);
</script>

<template>
  <v-text-field
    class="mb-4"
    :type="isInputHidden ? 'password' : 'text'"
    :append-inner-icon="isInputHidden ? 'mdi-eye-off' : 'mdi-eye'"
    :error-messages="v$.$errors.map((e) => e.$message)"
    :label="props.label"
    hide-details="auto"
    variant="outlined"
    @click:append-inner="() => (isInputHidden = !isInputHidden)"
    clearable
  ></v-text-field>
</template>
