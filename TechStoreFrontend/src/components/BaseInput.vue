<script setup>
const props = defineProps(["label", "v$"]);
</script>

<template>
  <v-text-field
    :error-messages="v$.$errors.map((e) => e.$message)"
    :label="props.label"
    variant="outlined"
  ></v-text-field>
</template>
