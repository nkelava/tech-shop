<script setup>
const props = defineProps(["label", "v$"]);
</script>

<template>
  <v-text-field
    class="mb-4"
    :error-messages="v$.$errors.map((e) => e.$message)"
    :label="props.label"
    hide-details="auto"
    variant="outlined"
    clearable
  ></v-text-field>
</template>
