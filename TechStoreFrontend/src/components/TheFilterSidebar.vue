<script setup>
import { ref, defineEmits } from "vue";

const emit = defineEmits(["filter"]);
const props = defineProps(["attributeValuesMap"]);
const price = ref({ from: 0, to: 0 });
const rating = ref(0);
const filterMap = ref(new Map());

const updateFilters = (event, attributeId, attributeValueId) => {
  if (!filterMap.value.has(attributeId)) {
    filterMap.value.set(attributeId, new Set([attributeValueId]));
    handleFilter();
    return;
  }

  if (filterMap.value.get(attributeId).has(attributeValueId)) {
    filterMap.value.get(attributeId).delete(attributeValueId);

    if (!filterMap.value.get(attributeId).size) {
      filterMap.value.delete(attributeId);
    }

    event.target.checked = false;
    handleFilter();
    return;
  }

  filterMap.value.get(attributeId).add(attributeValueId);
  handleFilter();
};

function handlepPriceFilter() {
  const { from, to } = price.value;

  if (from > to || from < 0 || to < 0) {
    return;
  }

  handleFilter();
}

function handleFilter() {
  emit("filter", price.value, rating.value, filterMap.value);
}
</script>

<template>
  <div class="sidebar">
    <div class="sidebar-item">
      <h3 class="sidebar-item__title">Price</h3>
      <hr />
      <div class="sidebar-price">
        <input class="sidebar-price__input" v-model="price.from" type="number" min="0" />
        <input
          class="sidebar-price__input"
          v-model="price.to"
          type="number"
          min="0"
          @input="handlepPriceFilter"
        />
      </div>
    </div>
    <div class="sidebar-item">
      <h3 class="sidebar-item__title">Rating</h3>
      <hr />
      <!-- TODO: create slider component in case of keeping this filter -->
      <div class="slider">
        <input v-model="rating" type="range" min="0" max="5" step="1" @change="handleFilter" />
        <span>{{ rating }}</span>
      </div>
    </div>
    <div
      class="sidebar-item"
      v-for="[attributeId, attribute] in attributeValuesMap"
      :key="attributeId"
    >
      <h3 class="sidebar-item__title">{{ attribute.name }}</h3>
      <hr />
      <div v-for="[attributeValueId, attributeValue] in attribute.values" :key="attributeValueId">
        <input
          type="checkbox"
          id="checkbox"
          @click="updateFilters($event, attributeId, attributeValueId)"
        />
        <label for="checkbox"> {{ attributeValue }} </label>
      </div>
    </div>
  </div>
</template>

<style scoped>
input[type="radio"],
input[type="checkbox"],
input[type="range"] {
  accent-color: var(--ts-c-bg-dark);
}

.sidebar {
  background-color: var(--ts-c-bg-light);
  border-radius: 10px;
  color: var(--ts-c-text-dark);
  display: flex;
  flex-direction: column;
}

.sidebar-item {
  margin-inline: 20px;
  margin: 1rem 2rem;
}

.sidebar-item__title {
  margin: 5px;
}

hr {
  margin-bottom: 1rem;
  margin-right: 2rem;
}

.sidebar-price {
  display: flex;
  flex-direction: column;
  padding-right: 2rem;
}

.sidebar-price__input {
  border: none;
  background-color: #fff;
  border-radius: 5px;
  font-size: 14px;
  margin: 5px 0;
  padding: 5px 10px;
  width: 100%;
  max-width: 250px;
  outline: none;
}

input::-webkit-outer-spin-button,
input::-webkit-inner-spin-button {
  -webkit-appearance: none;
  margin: 0;
}

.slider {
  display: flex;
  align-items: center;
}

input[type="range"] {
  width: 100%;
  max-width: 235px;
  margin-right: 10px;
}

input[type="checkbox"] {
  margin-right: 10px;
}

button {
  height: 30px;
  width: 100%;
}

@media only screen and (48em <= width <= 1024px) {
  .sidebar-price {
    flex-direction: row;
    justify-content: space-between;
  }
}
</style>
