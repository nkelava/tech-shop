<script setup>
import { ref } from "vue";

const props = defineProps(["label", "v$"]);

const isInputHidden = ref(true);
</script>

<template>
  <v-text-field
    :type="isInputHidden ? 'password' : 'text'"
    :append-icon="isInputHidden ? 'mdi-eye-off' : 'mdi-eye'"
    :error-messages="v$.$errors.map((e) => e.$message)"
    :label="props.label"
    @click:append="() => (isInputHidden = !isInputHidden)"
    variant="outlined"
  ></v-text-field>
</template>
