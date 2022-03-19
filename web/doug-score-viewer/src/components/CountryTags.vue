<template>
  <div class="field">
    <label class="label">Country</label>
    <div class="tags">
      <div class="tag is-white m-2 p-1 is-clickable tooltip" @click="toggleTag(name)" v-for="{ icon, name, selected } in props.originCountries">
        <span class="tooltiptext">{{ name }}</span>
        <figure class="image is-32x32">
          <img :src="icon" :alt="name" :class="{ 'is-selected': selected }" />
        </figure>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { PropType } from "vue";
import { Country } from "../models/searchQuery";

const props = defineProps({
  originCountries: Array as PropType<Country[]>,
});

function toggleTag(name: string) {
  const tag = props.originCountries!.find((e) => e.name === name);
  tag!.selected = !tag!.selected;
}
</script>
<style lang="scss" scoped>
.tag {
  img {
    opacity: 0.2;
  }

  img.is-selected {
    opacity: 1;
  }
}
</style>
